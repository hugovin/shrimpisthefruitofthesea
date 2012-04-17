using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;

public class CartAdditionInfo : ConnectSql
    {
    public CartAdditionInfo()
        {
            Close();
        }
    public  void addcartAdditionalInfo(int cartid, string state, string sku,
                                                int spanish,string whiteboard, string repsystem,string description)
    {
        try
        {

            Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DataBase;
            //Procedure Name.
            cmd.CommandText = "sp_add_torch_configurations";
            cmd.CommandType = CommandType.StoredProcedure;
            //Procedure Parameters.
            cmd.Parameters.Add("@cartid", SqlDbType.Int);
            cmd.Parameters.Add("@state", SqlDbType.VarChar, 50, "state");
            cmd.Parameters.Add("@sku", SqlDbType.VarChar, 50, "sku");
            cmd.Parameters.Add("@spanish", SqlDbType.Int);
            cmd.Parameters.Add("@whiteboard", SqlDbType.VarChar, 50, "whiteboard");
            cmd.Parameters.Add("@repsystem", SqlDbType.VarChar, 50, "repsystem");
            cmd.Parameters.Add("@description", SqlDbType.VarChar, 2000, "description");
            //Setting values to Parameters.
            cmd.Parameters[0].Value = cartid;
            cmd.Parameters[1].Value = state;
            cmd.Parameters[2].Value = sku;
            cmd.Parameters[3].Value = spanish;
            cmd.Parameters[4].Value = whiteboard;
            cmd.Parameters[5].Value = repsystem;
            cmd.Parameters[6].Value = description;
            cmd.ExecuteNonQuery();
            Close();
        }
        catch (SqlException oSqlExp)
        {
            //Console.WriteLine("" + oSqlExp.Message);
        }
        catch (Exception oEx)
        {
            //Console.WriteLine("" + oEx.Message);
        }

    }
    }

