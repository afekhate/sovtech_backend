using HRSolution.Infrastructure.ApiResponse;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sovtech.Infrastructure.ApiResponse;
using Sovtech.Utilities;
using Sovtech_HM.Utilities;
using System.Reflection;
using VatPay.Utilities.Common;


[ApiController]
public class BaseController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly ILogger _logger;


    public BaseController(IConfiguration config, ILogger<BaseController> logger)
    {

        _config = config;
        _logger = logger;

    }

    [HttpGet]
    [Route("/chuck/categories")]
    [ProducesResponseType(typeof(BaseResult<IList<ChuknorResponse>>), 200)]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> categories()
    {
        try
        {

            var Categories = await ApiMiddleWare.IRestGetAsync(_config["chukBaseUrl"], _config["Categories"]);
            var Response = JsonConvert.DeserializeObject<List<string>>(Categories.Content);



            var allCategories = new ChuknorResponse();
            int Id = 1;
            
            foreach (var item in Response)
            {
                var category = new Category { category = item, Id =  Id};
                allCategories.Categories.Add(category);
                Id++;
            }
           


            if (Categories.IsSuccessful == false)
            {
                return Ok(
                   new BaseResult<IList<ChuknorResponse>>
                   {
                       Output = null,
                       HasError = true,
                       Message = ApplicationResponseCode.LoadErrorMessageByCode("1000").Name,
                       StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("1000").Code
                   });
            }

            if (Response.Count != 0)
            {
                return Ok(
                    new StarWarBase<ChuknorResponse>
                    {
                        Result = allCategories, 
                        Message = ApplicationResponseCode.LoadErrorMessageByCode("200").Name,
                        StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("200").Code

                    });
            }

            else
            {
                return Ok(
                   new BaseResult<IList<ChuknorResponse>>
                   {
                       HasError = false,
                       Output = Response,
                       Message = ApplicationResponseCode.LoadErrorMessageByCode("200").Name,
                       StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("200").Code

                   });
            }

        }
        catch (Exception ex)
        {
            var u = new BaseResult<IList<ChuknorResponse>>
            {

                Output = null,
                HasError = true,
                Message = ex.Message,
                StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("1000").Code
            };
            return BadRequest(u);
        }

    }


    [HttpGet]
    [Route("/swapi/people")]
    [ProducesResponseType(typeof(StarWarBase<Root>), 200)]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> people()
    {
        try
        {

            var People = await ApiMiddleWare.IRestGetAsync(_config["swapBaseUrl"], _config["People"]);
            var Response = JsonConvert.DeserializeObject<Root>(People.Content);

            if (People.IsSuccessful == false)
            {
                return Ok(
                   new StarWarBase<IList<Root>>
                   {
                       Result = null,
                       HasError = true,
                       Message = ApplicationResponseCode.LoadErrorMessageByCode("1000").Name,
                       StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("1000").Code
                   });
            }

            if (Response.count != 0)
            {
                return Ok(
                    new StarWarBase<Root>
                    {
                        Result = Response,
                        Message = ApplicationResponseCode.LoadErrorMessageByCode("200").Name,
                        StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("200").Code

                    });
            }

            else
            {
                return Ok(
                   new StarWarBase<Root>
                   {
                       Result = Response,
                       Message = ApplicationResponseCode.LoadErrorMessageByCode("200").Name,
                       StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("200").Code

                   });
            }

        }
        catch (Exception ex)
        {
            var u = new StarWarBase<Root>
            {

                Result = null,
                Message = ex.Message,
                StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("1000").Code
            };
            return BadRequest(u);
        }

    }


    [HttpGet]
    [Route("/search")]
    [ProducesResponseType(typeof(StarWarBase<Root>), 200)]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> query([FromQuery] string query)
    {
        try
        {

            var _genral = new General();
            var columns = _genral.GetColumns();
            var match = string.Empty;
            var obj = string.Empty;
            var searchQuery = string.Empty;

            if (query.Contains(':'))
            {
                obj = JsonConvert.DeserializeObject<string>(query.Split(':')[0].ToString());
                searchQuery = JsonConvert.DeserializeObject<string>(query.Split(':')[1].ToString());
            }
            else
            {
                searchQuery = query;
            }


            //Check SwapApi
            var People = await ApiMiddleWare.IRestGetAsync(_config["swapBaseUrl"], _config["People"]);
            var Response = JsonConvert.DeserializeObject<Root>(People.Content);

            var queryResult = Response.results.Where(x => x.name == searchQuery || x.hair_color == searchQuery || x.homeworld == searchQuery || x.birth_year == searchQuery || x.eye_color == searchQuery  || x.height == searchQuery || x.homeworld == searchQuery || x.mass == searchQuery || x.skin_color == searchQuery);
               
            if(queryResult.Count() == 0)
            {

                //Check Chuck
                var Categories = await ApiMiddleWare.IRestGetAsync(_config["chukBaseUrl"], _config["Categories"]);
                var response = JsonConvert.DeserializeObject<List<string>>(Categories.Content);

                var queryresult = response.Contains(searchQuery);
                if (queryresult == true)
                {
                    return Ok(
                    new BaseResult<ChuknorResponse>
                    {
                        metadata = _config["chukBaseUrl"],
                        Output = new List<string>() { searchQuery },
                        Message = ApplicationResponseCode.LoadErrorMessageByCode("200").Name,
                        StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("200").Code

                    });
                }
                else
                {
                    return Ok(
                    new StarWarBase<Root>
                    {
                        Result = null,
                        Message = ApplicationResponseCode.LoadErrorMessageByCode("404").Name,
                        StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("404").Code

                    });
                }


            }
            else
            {
                return Ok(
                    new StarWarBase<IList<Result>>
                    {
                        Result = queryResult.ToList(),
                        metadata = _config["swapBaseUrl"],
                        HasError = false,
                        Message = ApplicationResponseCode.LoadErrorMessageByCode("200").Name,
                        StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("200").Code
                    });
            }
           
        }
        catch (Exception ex)
        {
            var u = new StarWarBase<Root>
            {

                Result = null,
                Message = ex.Message,
                StatusCode = ApplicationResponseCode.LoadErrorMessageByCode("1000").Code
            };
            return BadRequest(u);
        }

    }
}