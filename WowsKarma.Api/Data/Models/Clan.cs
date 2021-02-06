using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Wargaming.WebAPI.Models.WorldOfWarships.Responses;

namespace WowsKarma.Api.Data.Models
{
	public class Clan
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public uint Id { get; init; }

		public DateTime CreatedAt { get; init; }
		public DateTime DisbandedAt { get; init; }

		public string Name { get; set; }
		public string Tag { get; set; }
		public string Description { get; set; }
		public int Color { get; set; }

//		public List<ClanPlayer> Players { get; set; }
	}
}
