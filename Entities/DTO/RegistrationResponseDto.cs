using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTO
{
    public class RegistrationResponseDto
    {
        public bool SuccessfullRegistration { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
