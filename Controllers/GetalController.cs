using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace WebApiOef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetalController : ControllerBase
    {
        public string fileNumber = @"C:\Users\Public\Documents\getal.txt";
        public GetalController() 
        {
            List<string> numberList = new List<string>();
            numberList.Add("25");
            System.IO.File.WriteAllLines(fileNumber, numberList);
        }

        [HttpGet]
        public ActionResult<int> GetNumber() 
        {
            if (System.IO.File.Exists(fileNumber) == false) 
            {
                return NotFound();
            }
            List<string> numberList = new List<string>();
            numberList = System.IO.File.ReadAllLines(fileNumber).ToList();
            int number = int.Parse(numberList[0]);
            return number;
        }

        [HttpPost("Post1")]
        public ActionResult<int> PutInNumber(int yourNumber) 
        {
            if (System.IO.File.Exists(fileNumber) == false)
            {
                return NotFound();
            }
            List<string> numberList = new List<string>();
            numberList.Add(yourNumber.ToString());
            System.IO.File.WriteAllLines(fileNumber, numberList);

            numberList = System.IO.File.ReadAllLines(fileNumber).ToList();
            int number = int.Parse(numberList[0]);
            return number;
        }

        [HttpPost("Post2")]
        public ActionResult<int> GetRandomNumber()
        {
            if (System.IO.File.Exists(fileNumber) == false)
            {
                return NotFound();
            }
            var rng = new Random();
            List<string> numberList = new List<string>();
            numberList.Add(rng.Next(0, 1000).ToString());
            System.IO.File.WriteAllLines(fileNumber, numberList);

            numberList = System.IO.File.ReadAllLines(fileNumber).ToList();
            int number = int.Parse(numberList[0]);
            return number;
        }

    }
}
