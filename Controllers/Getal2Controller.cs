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
    public class Getal2Controller : ControllerBase
    {
        public string fileNumber = @"C:\Users\Public\Documents\getal.txt";
        public Getal2Controller()
        {
            if (!System.IO.File.Exists(fileNumber))
            {
                List<string> numberList = new List<string>();
                numberList.Add("25");
                numberList.Add("35");
                System.IO.File.WriteAllLines(fileNumber, numberList);
            }
        }

        [HttpGet("Get")]
        public ActionResult<List<int>> GetNumber()
        {
            if (!System.IO.File.Exists(fileNumber))
            {
                return NotFound();
            }
            List<int> numberList = new List<int>();
            List<string> stringList = System.IO.File.ReadAllLines(fileNumber).ToList();
            if(stringList.Count() == 0) { return NotFound(); }
            foreach(var number in stringList) 
            {
                numberList.Add(Convert.ToInt32(number));
            }
            return numberList;
        }

        [HttpPost("PostNewNumber")]
        public ActionResult<string> PutInNumber(int yourNumber)
        {
            if (!System.IO.File.Exists(fileNumber))
            {
                return NotFound();
            }
            List<string> stringList = System.IO.File.ReadAllLines(fileNumber).ToList();
            string yourNumberString = Convert.ToString(yourNumber);
            stringList.Add(yourNumberString);
            System.IO.File.WriteAllLines(fileNumber, stringList);
            return "You correctly entered your number: " + yourNumberString;
        }

        [HttpPut("ChangeNumber")]
        public ActionResult<string> ChangeNumber(int lineNumber, int newNumber) 
        {
            if (!System.IO.File.Exists(fileNumber))
            {
                return NotFound();
            }
            List<string> stringList = System.IO.File.ReadAllLines(fileNumber).ToList();
            if (stringList.Count() == 0) { return NotFound(); }
            if(lineNumber - 1 < 0 || lineNumber > stringList.Count()) { return BadRequest(); }
            string oldNumber = stringList.ElementAt(lineNumber - 1);
            stringList.RemoveAt(lineNumber - 1);
            stringList.Insert(lineNumber - 1, newNumber.ToString());
            System.IO.File.WriteAllLines(fileNumber, stringList);

            return "You correctly changed number: " + oldNumber + " to the new number " + newNumber;
        }

        [HttpDelete("Delete")]
        public ActionResult<string> DeleteFirstNumber() 
        {
            if (!System.IO.File.Exists(fileNumber))
            {
                return NotFound();
            }
            List<string> stringList = System.IO.File.ReadAllLines(fileNumber).ToList();
            if(stringList.Count() == 0) { return NotFound(); }
            string firstNumber = stringList.ElementAt(0);
            stringList.RemoveAt(0);
            System.IO.File.WriteAllLines(fileNumber, stringList);
            return "You correctly deleted number: " + firstNumber;
        }

        [HttpDelete("DeleteSpecific")]
        public ActionResult<string> DeleteSpecificNumber(int lineNumber) 
        {
            if (!System.IO.File.Exists(fileNumber))
            {
                return NotFound();
            }
            List<string> stringList = System.IO.File.ReadAllLines(fileNumber).ToList();
            if (stringList.Count() == 0) { return NotFound(); }
            if (lineNumber - 1 < 0 || lineNumber > stringList.Count()) { return BadRequest(); }
            string specificNumber = stringList.ElementAt(lineNumber - 1);
            stringList.RemoveAt(lineNumber - 1);
            System.IO.File.WriteAllLines(fileNumber, stringList);
            return "You correctly deleted number: " + specificNumber;
        }
        
    }
}
