using ConvertoApi.Models;
using ConvertoApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using ConvertoApi.Utilities;
using System.ComponentModel.DataAnnotations;

namespace ConvertoApi.Controllers
{
    /// <summary>
    /// Controller for converting numbers to words.
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    public class ConvertController : ControllerBase
    {
        private readonly ILogger<ConvertController> _logger;

        public ConvertController(ILogger<ConvertController> logger)
        {
            _logger = logger;
        }

        /// ----------------------------------------------------------------------------------------------------------------
        /// * Method Description : Converts a numerical input into words.
        /// ----------------------------------------------------------------------------------------------------------------
        /// * Change Summary
        /// * --------------
        ///   ID                Changed By         Changed On      Change Description
        ///*=======             ==========         ==========      ==================
        ///*RZM-127                Rizam          19-July-2024     Converts a numerical input into words Controller
        /// 

        /// <summary>
        /// Converts a numerical input into words.
        /// </summary>
        /// <param name="input">The numerical input to be converted.</param>
        /// <returns>The words representation of the numerical input.</returns>
        /// <response code="200">Returns the words representation of the numerical input</response>
        /// <response code="400">If the input is invalid</response>
        [HttpPost]
       // [ProducesResponseType(200)]
       // [ProducesResponseType(400)]
        public ActionResult<string> ConvertNumberToWords([FromBody] NumberInput input)
        {

            if (input == null)
            {
                _logger.LogWarning("Invalid input received.");
                return BadRequest("Invalid input.");
            }


            _logger.LogInformation("Received request to convert number: {Number}", input.Number);

            var result = NumberConverter.Convert(input.Number);

            _logger.LogInformation("Conversion result: {Result}", result);

            return Ok(result);
        }
    }
}
