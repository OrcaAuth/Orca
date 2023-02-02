﻿using System;

namespace Balea.Model
{
	public class Delegation
	{
		public string Who { get; set; }
		public string Whom { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public bool Active => From <= DateTime.UtcNow && To >= DateTime.UtcNow;
	}
}
