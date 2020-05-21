using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OptionPredicting.Models
{
    public class MonteCarloModel
    {
        [Required]
        public double S0 { get; set; }       
        [Required]
        public double K { get; set; }        
        [Required]
        public double sigma { get; set; }       
        [Required]
        public double r { get; set; }       
        [Required]
        public double T { get; set; }        
        [Required]
        public double timestamp { get; set; }       
        [Required]
        public double samples { get; set; }        
    }
}
