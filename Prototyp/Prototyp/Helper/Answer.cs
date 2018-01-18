using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helper;

namespace Prototyp.Helper
{
	public class Answer
	{
		public int Rating { get; set; }

		public String AnswerText { get; set; }
		public IntuitionEffect AnswerEffect { get; set; }
	}
}