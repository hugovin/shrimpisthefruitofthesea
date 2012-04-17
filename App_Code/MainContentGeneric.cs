using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for MainContentGeneric
/// </summary>
public class MainContentGeneric
{
    private int SiteId = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
    private int ContId = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());

    public MainContentGeneric()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //Generic A
    //public DataSet getAllGenericByTypeId(int GenId, int GeneDefaId)
    public DataSet getAllGenericByTypeId(int GenId)
    {
        Gen_A generic_A = new Gen_A();
        //return generic_A.get_GenA_By_Gene_Id(SiteId, ContId, GenId, GeneDefaId);
        return generic_A.get_GenA_By_Gene_Id(SiteId, ContId, GenId);
    }

    //Generic B
    //public DataSet getAllGenericResourcesInfo(int GenId, int GeneDefaId)
    public DataSet getAllGenericResourcesInfo(int GenId)
    {
        Gen_B resourcesInfo = new Gen_B();
        //return resourcesInfo.get_Resources_Info(SiteId, ContId, GenId, GeneDefaId);
        return resourcesInfo.get_Resources_Info(SiteId, ContId, GenId);
    }

    public DataSet Get_Site_Generic_B(int GeneId)
    {
        Gen_B generic_B = new Gen_B();
        return generic_B.Get_Site_Generic_B(SiteId, ContId, GeneId);
    }

    public DataSet Get_Generic_B_By_GenBId(int GenBId)
    {
        Gen_B generic_B = new Gen_B();
        return generic_B.Get_Generic_B_By_GenBId(GenBId);
    }

    //Generic C    
    public DataSet getAllGenericResourcesFaqs(int GenCId)
    {
        Gen_C generic_C = new Gen_C();        
        return generic_C.Get_Generic_Resources_Faqs(SiteId, ContId, GenCId);
    }

    public DataSet Get_Site_Generic_C(int GenCId)
    {
        Gen_C generic_C = new Gen_C();
        return generic_C.Get_Site_Generic_C(SiteId, ContId, GenCId);
    }

    //Generic D    
    public DataSet getAllGenericResourcesPurchasing(int GenDId)
    {
        Gen_D generic_D = new Gen_D();      
        return generic_D.Get_Generic_Resources_Purchasing(SiteId, ContId, GenDId);
    }

    public DataSet Get_Site_Generic_D(int GenDId)
    {
        Gen_D generic_D = new Gen_D();
        return generic_D.Get_Site_Generic_D(SiteId, ContId, GenDId);
    }

    public DataSet getAllGenericDByTypeId(int GenDId)
    {
        Gen_D generic_D = new Gen_D();
        return generic_D.Get_Generic_D_By_Type(SiteId, GenDId);
    }

    //Generic E
    public DataSet getAllGenericEByTypeId(int GenEId)
    {
        Gen_E generic_E = new Gen_E();
        return generic_E.Get_Generic_E_By_Type(SiteId, GenEId);
    }

    public DataSet Get_Site_Generic_E(int GenEId)
    {
        Gen_E generic_E = new Gen_E();
        return generic_E.Get_Site_Generic_E(SiteId, ContId, GenEId);
    }

    //Generic X   
    public DataSet getAllGenericXByTypeId(int GenXId)
    {
        Gen_X generic_X = new Gen_X();
        return generic_X.Get_Generic_X_By_Type(SiteId, GenXId);
    }

    public DataSet Get_GenericX_By_Id(int GenXId)
    {
        Gen_X generic_X = new Gen_X();
        return generic_X.Get_GenericX_By_Id(GenXId);
    }

    //Extrae tipo de menú 
    public DataSet getGeneTypeId(string _gentype)
    {
        Generics generics = new Generics();
        return generics.Get_GeneDefault_By_Id(_gentype);
    }

    //Get Id Generic by Default Id
    public DataSet Get_Id_Generic(int genDefaId)
    {
        Generics generics = new Generics();
        return generics.Get_Id_Generic(SiteId, ContId, genDefaId);
    }
}
