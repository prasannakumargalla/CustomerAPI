using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        public CustomerController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet(Name = "GetCustomer")]
        public IEnumerable<clsCustomer> Get()
        {
            List<clsCustomer> liCustomer = new List<clsCustomer>();
            liCustomer.Add(new clsCustomer() { title = "Prasanna", description = "Galla" });
            return liCustomer.AsEnumerable();

        }

        [HttpPost]
        public void PostTodoItem(clsCustomer todoDTO)
        {

            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            //string connectionString = @"Data Source=PRASANNA;Initial Catalog=EcommerceDB;Integrated Security=True";

            using (var con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Insert_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", todoDTO.title);
                cmd.Parameters.AddWithValue("@LastName", todoDTO.description);
               
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

           
        }
    }
}
