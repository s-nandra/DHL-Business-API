using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHL_WS_Test.GeschaeftskundenversandWS;

namespace Geschaeftskundenversand
{
    class GeschaeftskundenversandRequestBuilder
    {
        private static String SHIPPER_STREET = "Heinrich-Bruening-Str.";
        private static String SHIPPER_STREETNR = "7";
        private static String SHIPPER_CITY = "Bonn";
        private static String SHIPPER_ZIP = "53113";
        private static String SHIPPER_COUNTRY_CODE = "DE";
        private static String SHIPPER_CONTACT_EMAIL = "max@muster.de";
        private static String SHIPPER_CONTACT_NAME = "Max Muster";
        private static String SHIPPER_CONTACT_PHONE = "030244547777778";
        private static String SHIPPER_COMPANY_NAME = "Deutsche Post IT Brief GmbH";
        //	private static String ENCODING = "UTF8";
        private static String MAJOR_RELEASE = "2";
        private static String MINOR_RELEASE = "0";
        private static String SDF = "yyyy-MM-dd";
        private static String DD_PROD_CODE = "EPN";
        private static String TD_PROD_CODE = "WPX";
        private static String EKP = "5000000008";
        private static String PRODUCT_CODE = "V01PAK";
        private static String BILLING_NUMBER = "22222222220101";
        private static String PARTNER_ID = "01";
        private static String SHIPMENT_DESC = "Interessanter Artikel";
        private static String TD_SHIPMENT_REF = "DDU";
        private static float TD_VALUE_GOODS = 250;
        private static String TD_CURRENCY = "EUR";
        private static String TD_ACC_NUMBER_EXPRESS = "141075589";


        //Beispieldaten Für DD-Sendungen aus/nach Deutschland
        private static String RECEIVER_FIRST_NAME = "Kai";
        private static String RECEIVER_LAST_NAME = "Wahn";
        private static String RECEIVER_LOCAL_STREET = "Marktplatz";
        private static String RECEIVER_LOCAL_STREETNR = "1";
        private static String RECEIVER_LOCAL_CITY = "Stuttgart";
        private static String RECEIVER_LOCAL_ZIP = "70173";
        private static String RECEIVER_LOCAL_COUNTRY_CODE = "DE";

        //Beispieldaten Für TD-Sendungen weltweit
        private static String RECEIVER_WWIDE_STREET = "Chung Hsiao East Road.";
        private static String RECEIVER_WWIDE_STREETNR = "55";
        private static String RECEIVER_WWIDE_CITY = "Taipeh";
        private static String RECEIVER_WWIDE_ZIP = "100";
        private static String RECEIVER_WWIDE_COUNTRY = "Taiwan";
        private static String RECEIVER_WWIDE_COUNTRY_CODE = "TW";

        private static String RECEIVER_CONTACT_EMAIL = "kai@wahn.de";
        private static String RECEIVER_CONTACT_NAME = "Kai Wahn";
        private static String RECEIVER_CONTACT_PHONE = "+886 2 27781-8";
        private static String RECEIVER_COMPANY_NAME = "Klammer Company";
        private static String DUMMY_SHIPMENT_NUMBER = "0000000";

        private static String EXPORT_REASON = "Sale";
        private static String SIGNER_TITLE = "Director Asia Sales";
        private static String INVOICE_NUMBER = "200601xx417";
        private static String DUMMY_AIRWAY_BILL = "0000000000";

        public static DHL_WS_Test.GeschaeftskundenversandWS.Version createVersion()
        {
            DHL_WS_Test.GeschaeftskundenversandWS.Version version = new DHL_WS_Test.GeschaeftskundenversandWS.Version();
            version.majorRelease = MAJOR_RELEASE;
            version.minorRelease = MINOR_RELEASE;
            return version;
        }

        public static NativeAddressType createShipperNativeAddressType()
        {

            NativeAddressType address = new NativeAddressType();
            address.streetName = SHIPPER_STREET;
            address.streetNumber = SHIPPER_STREETNR;
            address.city = SHIPPER_CITY;
            address.zip = SHIPPER_ZIP;
            CountryType origin = new CountryType();
            origin.countryISOCode = SHIPPER_COUNTRY_CODE;
            address.Origin = origin;


            return address;
        }

        public static ReceiverNativeAddressType createReceiverNativeAddressType()
        {

            ReceiverNativeAddressType address = new ReceiverNativeAddressType();
            CountryType origin = new CountryType();
            address.streetName = RECEIVER_LOCAL_STREET;
            address.streetNumber = RECEIVER_LOCAL_STREETNR;
            address.city = RECEIVER_LOCAL_CITY;
            address.zip = RECEIVER_LOCAL_ZIP;
            origin.countryISOCode = RECEIVER_LOCAL_COUNTRY_CODE;
            address.Origin = origin;
            return address;
        }


        public static CommunicationType createShipperCommunicationType()
        {
            CommunicationType communication = new CommunicationType();
            communication.email = SHIPPER_CONTACT_EMAIL;
            communication.contactPerson = SHIPPER_CONTACT_NAME;
            communication.phone = SHIPPER_CONTACT_PHONE;
            return communication;
        }


        public static CommunicationType createReceiverCommunicationType()
        {
            CommunicationType communication = new CommunicationType();
            communication.email = RECEIVER_CONTACT_EMAIL;
            communication.contactPerson = RECEIVER_CONTACT_NAME;
            communication.phone = RECEIVER_CONTACT_PHONE;
            return communication;
        }

        public static NameType createShipperCompany()
        {
            NameType name = new NameType();
            name.name1 = SHIPPER_COMPANY_NAME;
            return name;
        }


        public static NameType createReceiverCompany()
        {
            NameType name = new NameType();
            name.name1 = RECEIVER_FIRST_NAME + " " + RECEIVER_LAST_NAME;
            name.name2 = RECEIVER_COMPANY_NAME;
            return name;
        }


        public static ShipmentDetailsTypeType createShipmentDetailsType()
        {
            DateTime today = DateTime.Today;
            today.AddDays(2);

            ShipmentDetailsTypeType shipmentDetails = new ShipmentDetailsTypeType();
            shipmentDetails.product = PRODUCT_CODE;
            shipmentDetails.shipmentDate = today.ToString(SDF);
            shipmentDetails.accountNumber = BILLING_NUMBER;

            ShipmentItemType shItemType = new ShipmentItemType();
            shipmentDetails.ShipmentItem = shItemType;
            shipmentDetails.ShipmentItem = createDefaultShipmentItemType();
            shipmentDetails.customerReference = SHIPMENT_DESC;

            return shipmentDetails;
        }


        public static ShipmentItemType createDefaultShipmentItemType()
        {
            ShipmentItemType shipmentItem = new ShipmentItemType();
            shipmentItem.weightInKG = Decimal.Parse("3");
            shipmentItem.lengthInCM = "50";
            shipmentItem.widthInCM = "30";
            shipmentItem.heightInCM = "15";
            return shipmentItem;
        }




        public static CreateShipmentOrderRequest createDefaultShipmentOrderRequest()
        {

            // create empty request
            CreateShipmentOrderRequest createShipmentOrderRequest = new CreateShipmentOrderRequest();
            // set version element


            createShipmentOrderRequest.Version = createVersion();
            // create shipment order object
            ShipmentOrderType shipmentOrderType = new ShipmentOrderType();


            shipmentOrderType.sequenceNumber = "1";

            ShipmentOrderTypeShipment shipment = new ShipmentOrderTypeShipment();
            shipmentOrderType.Shipment = shipment;
            shipment.ShipmentDetails = createShipmentDetailsType();

            ShipperType shipper = new ShipperType();

            shipper.Name = createShipperCompany();
            shipper.Address = createShipperNativeAddressType();
            shipper.Communication = createShipperCommunicationType();
            shipment.Shipper = shipper;

            ReceiverType receiver = new ReceiverType();

            receiver.name1 = createReceiverCompany().name1;
            receiver.Item = createReceiverNativeAddressType();
            receiver.Communication = createReceiverCommunicationType();

            shipment.Receiver = receiver;

            shipmentOrderType.labelResponseType = ShipmentOrderTypeLabelResponseType.URL;

            ShipmentOrderType[] shOrder = new ShipmentOrderType[1];

            // Shipment Order zum Request hinzufügen
            shOrder[0] = shipmentOrderType;
            createShipmentOrderRequest.ShipmentOrder = shOrder;
            return createShipmentOrderRequest;
        }


        public static GetLabelRequest getDefaultLabelRequest(String shipmentId)
        {
            GetLabelRequest Request = new GetLabelRequest();
            Request.Version = createVersion();
            String shNumber = shipmentId;

            String[] shNumbers = new String[1];
            shNumbers[0] = shNumber;
            Request.shipmentNumber = shNumbers;
            return Request;
        }


        public static DeleteShipmentOrderRequest getDeleteShipmentOrcerRequest(
                String shipmentId)
        {
            DeleteShipmentOrderRequest Request = new DeleteShipmentOrderRequest();
            Request.Version = createVersion();
            String[] shNumbers = new String[1];
            shNumbers[0] = shipmentId;
            Request.shipmentNumber = shNumbers;
            return Request;
        }
    }
}
