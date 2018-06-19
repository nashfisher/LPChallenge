using System;
using System.ComponentModel.DataAnnotations;

namespace LPChallenge.Models
{
    public class SegmentViewModel
    {
        public string RequestId { get; set; }

        [Required (ErrorMessage = "Segments require a distance measurement")]
        public int? distance { get; set; }

        [Required (ErrorMessage = "Segments must include a feature")]
        [MinLength(1, ErrorMessage = "Segments must include a feature")]
        public string feature { get; set; }
    }
}