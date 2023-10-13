using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace inventory.Pages.Rangsit
{
    public class IndexRangsitModel : PageModel
    {
        public List<StockInfo> ListStorks = new List<StockInfo>();
        public void OnGet()
        {
            try {
                String connectionString = "Server=tcp:inventory1650701509.database.windows.net,1433;Initial Catalog=invensitory1650701509;Persist Security Info=False;User ID=chatchawanDatabase404;Password=12345@Dew;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    String sql = "SELECT * FROM stocks WHERE storeid=1";

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader render = command.ExecuteReader()) {

                            while (render.Read()) {
                                StockInfo stockInfo = new StockInfo();
                                stockInfo.itemid = "" + render.GetInt32(0);
                                stockInfo.item = render.GetString(1);
                                stockInfo.storeid = render.GetString(2);
                                stockInfo.supplier = render.GetString(3);
                                stockInfo.amount = render.GetString(4);
                                stockInfo.create_at = render.GetDateTime(5).ToString();

                                ListStorks.Add(stockInfo);

                            }
                        }
                    }

                } 
            }
            catch(Exception ex) {
                Console.WriteLine("Exception : " + ex.ToString());
            }
        }

        public class StockInfo {
            public String itemid;
            public String item;
            public String storeid;
            public String supplier;
            public String amount;
            public String create_at;
        }
    }
}
