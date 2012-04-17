using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.IO;
using System.Net;
using System.Text;
using System.Web.Util;
using System.Xml;
using System.Xml.XPath;

public class UPS
{
	private UPS(){}

    public static bool CalculateShippingCost(CartLocation ship_to, string ship_type, double weight, double min_cost, out double cost)
    {
        string url = "https://wwwcie.ups.com/ups.app/xml/Rate";
        //string url = "https://www.ups.com/ups.app/xml/Rate";
        double originalw = weight;
        if (weight < 1)
        {
            weight = 1;
        }

        #region Template XML
        string template = "<?xml version=\"1.0\"?>" +
        "<AccessRequest xml:lang=\"en-US\">" +
        @" 	<AccessLicenseNumber>{license}</AccessLicenseNumber>
	        <UserId>{user_id}</UserId>
	        <Password>{user_password}</Password>
        </AccessRequest>" +
        "<?xml version=\"1.0\"?>" +
        "<RatingServiceSelectionRequest xml:lang=\"en-US\">" +
        @"<Request>
            <TransactionReference>
              <CustomerContext>Rating and Service</CustomerContext>
              <XpciVersion>1.0</XpciVersion>
            </TransactionReference>
	        <RequestAction>Rate</RequestAction>
	        <RequestOption>Rate</RequestOption>
          </Request>
            <PickupType>
  	            <Code>07</Code>
  	            <Description>Rate</Description>
            </PickupType>
          <Shipment>
            <Description>Rate Description</Description>
            <Shipper>
              <Name>{shipper_name}</Name>
              <PhoneNumber>{shipper_phone}</PhoneNumber>
              <ShipperNumber>{shipper_number}</ShipperNumber>
              <Address>
                <AddressLine1>{shipper_address}</AddressLine1>
                <City>{shipper_city}</City>
                <StateProvinceCode>{shipper_state}</StateProvinceCode>
                <PostalCode>{shipper_postal}</PostalCode>
                <CountryCode>{shipper_country}</CountryCode>
              </Address>
            </Shipper>
            <ShipTo>
              <CompanyName>{shipto_company}</CompanyName>
              <PhoneNumber>{shipto_phone}</PhoneNumber>
              <Address>
                <AddressLine1>{shipto_address1}</AddressLine1>
                <AddressLine2>{shipto_address2}</AddressLine2>
                <City>{shipto_city}</City>
                <PostalCode>{shipto_postal}</PostalCode> 
                <CountryCode>{shipto_country}</CountryCode>
              </Address>
            </ShipTo>   
  	        <Service>
                <Code>{ship_type}</Code>
  	        </Service>
  	        <Package>
	            <PackagingType>
    	            <Code>02</Code>
		            <Description>Customer Supplied</Description>
	            </PackagingType>
	            <Description>Rate</Description>
	            <PackageWeight>
		            <UnitOfMeasurement>
		              <Code>LBS</Code>
		            </UnitOfMeasurement>
    	            <Weight>{pounds}</Weight>
	            </PackageWeight>   
   	        </Package>
          </Shipment>
        </RatingServiceSelectionRequest>";
        #endregion

        template = template.Replace("{user_id}", "jdesario");
        template = template.Replace("{user_password}", "edship15");
        template = template.Replace("{license}", "2C44986E2F8FE20C");

        template = template.Replace("{ship_type}", ship_type);

        template = template.Replace("{shipper_name}", "Educational Resources, Inc.");
        template = template.Replace("{shipper_phone}", "847-888-8300");
        template = template.Replace("{shipper_number}", "");
        template = template.Replace("{shipper_address}", "1550 Executive Drive");
        template = template.Replace("{shipper_city}", "Elgin");
        template = template.Replace("{shipper_state}", "IL");
        template = template.Replace("{shipper_postal}", "60123");
        template = template.Replace("{shipper_country}", "US");

        template = template.Replace("{shipto_company}", ship_to.BusinessName);
        template = template.Replace("{shipto_phone}", ship_to.Phone);
        template = template.Replace("{shipto_address1}", ship_to.Address1);
        template = template.Replace("{shipto_address2}", ship_to.Address2);
        template = template.Replace("{shipto_city}", ship_to.City);
        template = template.Replace("{shipto_state}", ship_to.StateCode);
        template = template.Replace("{shipto_postal}", ship_to.PostalCode);
        template = template.Replace("{shipto_country}", ship_to.CountryCode);

        template = template.Replace("{pounds}", weight.ToString());

        byte[] buffer = Encoding.ASCII.GetBytes(template);

        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
        req.ContentLength = buffer.Length;
        req.ContentType = "text/xml";
        req.Method = "POST";

        Stream s = req.GetRequestStream();
        s.Write(buffer, 0, buffer.Length);
        s.Close();
        
        HttpWebResponse resp = null;
        try
        {
            resp = (HttpWebResponse)req.GetResponse();
        }
        catch (System.Net.WebException ex)
        {
            throw new UPSException(String.Format("There was an error ({0}) getting shipping data", ex.Status));
        }

        if (resp != null && resp.StatusCode == HttpStatusCode.OK)
        {
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response);

            XPathNavigator nav = doc.CreateNavigator();
            XPathExpression status = nav.Compile("//ResponseStatusCode");
            int status_code = nav.SelectSingleNode(status).ValueAsInt;
            if (status_code != 1) throw new UPSException(String.Format("There was an error ({0})", status_code, template));

            XPathExpression exp = nav.Compile("//RatedShipment/TotalCharges/MonetaryValue");
            XPathNodeIterator iter = nav.Select(exp);

            while (iter.MoveNext())
            {
                cost = iter.Current.ValueAsDouble;
                if (cost < min_cost) cost = min_cost;
		if (originalw < 1) { cost = min_cost; }
                return true;
            }
        }

        cost = min_cost;
        return false;
    }
}

public class UPSException : System.ApplicationException
{
    public UPSException(string msg) : base(msg) { }
}