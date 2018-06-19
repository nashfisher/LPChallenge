using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LPChallenge.Models{
	
	public class Border: BaseEntity {
		
        public List<Segment> segments { get; set; }

	}

	public class Segment: BaseEntity {

        public int? BorderId { get; set; }
		public int? distance { get; set; }
		public string feature { get; set; }

	}
}

	
	


