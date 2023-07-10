using Api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Api.CheckAnswerMLModel;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiApiController : ControllerBase
    {
        [HttpPost("test")]
        public ApiResponse<ModelOutput> test([FromBody] ModelInput model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new ApiResponse<ModelOutput> { Success = false, Message = "Not Found" };

                var predictionResult = CheckAnswerMLModel.Predict(model);

                return new ApiResponse<ModelOutput> { Success = true, Message = "success", Data = predictionResult };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ModelOutput> { Success = false, Message = ex.Message };
            }

        }

    }
}
