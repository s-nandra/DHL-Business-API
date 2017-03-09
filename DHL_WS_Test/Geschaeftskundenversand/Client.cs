using DHL_WS_Test.GeschaeftskundenversandWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Geschaeftskundenversand
{
    class Client
    {

        private static String CRED_FILE_PATH = "./Geschaeftskundenversand/Einstellungen/zugangsdaten.ini";
        private static Encoding ENCODING = Encoding.UTF8;
        private static String BCS_USERNAME_PROPERTY_NAME = "bcs_user";
        private static String BCS_PASSWD_PROPERTY_NAME = "bcs_password";
        private static String CIG_USERNAME_PROPERTY_NAME = "cig_user";
        private static String CIG_PASSWD_PROPERTY_NAME = "cig_password";


        public static void startTestRequest()
        {
            Dictionary<String, String> credDict = CredentialsFileReader.getSettings(CRED_FILE_PATH, ENCODING);
            String is_username = credDict[BCS_USERNAME_PROPERTY_NAME];
            String is_passwd = credDict[BCS_PASSWD_PROPERTY_NAME];
            String cig_username;
            String cig_passwd;




            // Service-Stub erstellen
            GKVAPIServicePortTypeClient webService = new GKVAPIServicePortTypeClient("GKVAPISOAP11port0");

            // Endpoint auf Sandbox (https://cig.dhl.de/services/sandbox/soap) umkonfigurieren
            webService.Endpoint.Address = new System.ServiceModel.EndpointAddress("https://cig.dhl.de/services/sandbox/soap");
            // Endpoint auf Http BasicAuth konfigurien
            System.ServiceModel.BasicHttpBinding Binding = new System.ServiceModel.BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.Transport);
            Binding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Basic;
            // Settings übernehmen
            webService.Endpoint.Binding = Binding;

            // Prompt the user for username & password

            GetPassword(credDict, out cig_username, out cig_passwd);

            // Basic Auth User und Password setzen
            webService.ClientCredentials.UserName.UserName = cig_username;
            webService.ClientCredentials.UserName.Password = cig_passwd;

            AuthentificationType auth = new AuthentificationType();
            auth.user = is_username;
            auth.signature = is_passwd;

            try
            {
                short input = 0;
                do
                {
                    input = readMainMenuInput();
                    switch (input)
                    {
                        case 1:
                            runCreateShipmentOrderRequest(webService, auth);
                            break;
                        case 2:
                            runGetLabelRequest(webService, auth);
                            break;
                        case 3:
                            runDeleteShipmentRequest(webService, auth);
                            break;
                    }
                    if (input == -1)
                        Console.WriteLine("Please correct your entry!");
                    else if (input != 0)
                    {
                        Console.Write("Please, press the Enter key to continue.");
                        Console.ReadLine();
                    }
                } while (input != 0);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
            }

        }


        private static short readMainMenuInput()
        {
            Console.WriteLine("Geschaeftskundenversand Operationen: " + "\n" +
                "1 - runCreateShipmentOrderRequest" + "\n" +
                "2 - runGetLabelRequest" + "\n" +
                "3 - runDeleteShipmentRequest" + "\n" +
                "0 - Programm beenden" + "\n");
            Console.Write("Choose the desired operation: ");
            String choice = Console.ReadLine();
            try
            {
                return short.Parse(choice);
            }
            catch
            {
                return -1;
            }
        }


        private static void runDeleteShipmentRequest(GKVAPIServicePortTypeClient port, AuthentificationType auth)
        {
            Console.WriteLine("enter tracking number:");
            String Sendungsnummer = Console.ReadLine();
            DeleteShipmentOrderRequest ddRequest = GeschaeftskundenversandRequestBuilder.getDeleteShipmentOrcerRequest(Sendungsnummer);

            try
            {
                DeleteShipmentOrderResponse delResponse = port.deleteShipmentOrder(auth, ddRequest);

                //Response status
                Statusinformation status = delResponse.Status;
                String statusText = status.statusText;
                DeletionState[] delStates = delResponse.DeletionState;

                Console.Write("deleteShipmentOrderRequest: \n" +
                        "Status-msg: " + statusText + "\n");

                foreach (DeletionState delState in delStates)
                {
                    Console.Write("tracking number: " + delState.shipmentNumber + "\n" +
                            "Status: " + delState.Status.statusText + "\n" +
                            "Status-Code: " + delState.Status.statusCode + "\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Each key terminates the Program!");
                String wait = Console.ReadLine();
                Environment.Exit(0);
            }
        }


        private static void runCreateShipmentOrderRequest(GKVAPIServicePortTypeClient port, AuthentificationType auth)
        {
            CreateShipmentOrderRequest Request = GeschaeftskundenversandRequestBuilder.createDefaultShipmentOrderRequest();

            try
            {
                CreateShipmentOrderResponse shResponse = port.createShipmentOrder(auth, Request);

                //Response status
                Statusinformation status = shResponse.Status;
                String statusCode = status.statusCode;
                String statusText = status.statusText;
                String Shipmentnumber = shResponse.CreationState[0].LabelData.shipmentNumber;
                //Label URL
                Object labelURL = shResponse.CreationState[0].LabelData.Item;

                Console.Write("CreateShipmentDDRequest: \n" +
                        "Request Status: Code: " + statusCode + "\n" +
                        "Status-Nachricht: " + statusText + "\n" +
                        "Label URL: " + labelURL + "\n" +
                        "Shipmentnumber: " + Shipmentnumber + "\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Each key terminates the Program!");
                String wait = Console.ReadLine();
                Environment.Exit(0);
            }
        }



        private static void runGetLabelRequest(GKVAPIServicePortTypeClient port, AuthentificationType auth)
        {

            Console.WriteLine("Sendungsnummer eingeben:");
            String Sendungsnummer = Console.ReadLine();
            
            GetLabelRequest Request = GeschaeftskundenversandRequestBuilder.getDefaultLabelRequest(Sendungsnummer);

            try
            {
                GetLabelResponse lblResponse = port.getLabel(auth, Request);

                //Response status
                Statusinformation status = lblResponse.Status;
                String statusMessage = status.statusText;
                LabelData[] lblDataList = lblResponse.LabelData;

                Console.Write("geLabelDDRequest: \n" +
                        "Status-Nachricht: " + statusMessage + "\n");

                foreach (LabelData lblData in lblDataList)
                {
                    Console.Write("Sendungsnummer: " + lblData.shipmentNumber + "\n" +
                                        "Status: " + lblData.Status.statusText + "\n" +
                                        "Label URL: " + lblData.Item + "\n");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Jede Taste beendet das Program!");
                String wait = Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public static void GetPassword(Dictionary<String, String> credDict, out String username, out String password)
        {
            Console.WriteLine("Bitte EntwicklerID und Password prüfen");
            Console.WriteLine("EntwicklerID: " + credDict[CIG_USERNAME_PROPERTY_NAME]);
            Console.WriteLine("Password:     " + credDict[CIG_PASSWD_PROPERTY_NAME]);
            Console.WriteLine("Sind die Angaben korrekt? (Y/n):");
            String korrekt = "";
            korrekt = Console.ReadLine();
            if (korrekt == "" || korrekt.ToLower() == "y")
            {
                username = credDict[CIG_USERNAME_PROPERTY_NAME];
                password = credDict[CIG_PASSWD_PROPERTY_NAME];
            }
            else
            {
                Console.WriteLine("EntwicklerID eingeben:");
                username = Console.ReadLine();
                Console.WriteLine("Password eingeben:");
                password = Console.ReadLine();
            }
            return;
        }




    }
}
