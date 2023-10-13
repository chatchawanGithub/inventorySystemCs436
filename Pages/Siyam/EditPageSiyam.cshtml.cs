using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static inventory.Pages.Rangsit.IndexRangsitModel;

namespace inventory.Pages.Siyam
{
    public class EditPageSiyamModel : PageModel
    {
        public StockInfo stockInfo = new StockInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            string itemid = Request.Query["itemid"];

            try
            {
                String connectionString = "Server=tcp:inventory1650701509.database.windows.net,1433;Initial Catalog=invensitory1650701509;Persist Security Info=False;User ID=chatchawanDatabase404;Password=12345@Dew;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM stocks WHERE itemid=@itemid";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@itemid", itemid);
                        using (SqlDataReader render = command.ExecuteReader())
                        {

                            if (render.Read())
                            {
                                stockInfo.itemid = "" + render.GetInt32(0);
                                stockInfo.item = render.GetString(1);
                                stockInfo.storeid = render.GetString(2);
                                stockInfo.supplier = render.GetString(3);
                                stockInfo.amount = render.GetString(4);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            stockInfo.itemid = Request.Form["itemid"];
            stockInfo.item = Request.Form["item"];
            stockInfo.storeid = Request.Form["storeid"];
            stockInfo.supplier = Request.Form["supplier"];
            stockInfo.amount = Request.Form["amount"];

            if (stockInfo.item.Length == 0 || stockInfo.storeid.Length == 0 || stockInfo.supplier.Length == 0 || stockInfo.amount.Length == 0)
            {
                errorMessage = "All the field are require";
                return;
            }

            try
            {
                String connectionString = "Server=tcp:inventory1650701509.database.windows.net,1433;Initial Catalog=invensitory1650701509;Persist Security Info=False;User ID=chatchawanDatabase404;Password=12345@Dew;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE stocks " +
                        "SET item=@item , storeid=@storeid , supplier=@supplier , amount=@amount " +
                        "WHERE itemid=@itemid;"
                        ;

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@itemid", stockInfo.itemid);
                        command.Parameters.AddWithValue("@item", stockInfo.item);
                        command.Parameters.AddWithValue("@storeid", stockInfo.storeid);
                        command.Parameters.AddWithValue("@supplier", stockInfo.supplier);
                        command.Parameters.AddWithValue("@amount", stockInfo.amount);

                        command.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Siyam/IndexSiyam");
        }
    }
}

