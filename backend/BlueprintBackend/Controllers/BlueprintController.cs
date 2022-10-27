using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlueprintBackend.Interfaces;

namespace BlueprintBackend.Controllers
{

    [Route("api/blueprint")]
    [ApiController]
    public class BlueprintController : ControllerBase
    {
        private readonly IBlueprintService _blueprintService;

        public BlueprintController(IBlueprintService blueprintService)
        {
            this._blueprintService = blueprintService;
        }



        [HttpGet("test")]
        public string GetMedicalWorkers(string name)
        {
            return "Hello " + name;
        }

/*        [HttpPost("prescriptions/{prescriptionId}/fulfill")]
        public async Task<PrescriptionDto> FulFillPrescription(string prescriptionId)
        {
            return await _pharmacistService.FulFillPrescription(prescriptionId);
        }*/

    }
}

