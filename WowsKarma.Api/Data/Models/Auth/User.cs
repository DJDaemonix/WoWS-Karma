﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace WowsKarma.Api.Data.Models.Auth
{
	public record User
	{
		[Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public uint Id { get; init; }

		public List<Role> Roles { get; set; }

		[Required]
		public Guid SeedToken { get; set; }

		public DateTime LastTokenRequested { get; set; }
	}
}
