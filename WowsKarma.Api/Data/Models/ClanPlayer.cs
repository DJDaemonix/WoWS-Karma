using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wargaming.WebAPI.Models.WorldOfWarships.Responses;



namespace WowsKarma.Api.Data.Models
{
	public class ClanPlayer
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		public uint PlayerId { get; init; }
		public Player Player { get; set; }

		public uint ClanId { get; init; }
		public Clan Clan { get; set; }

		public DateTime JoinedAt { get; init; }

		public ClanRole Role { get; set; }
	}
}
