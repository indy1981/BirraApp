using BirrasApp.Repositories.Models;
using BirrasApp.Repositories.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BirrasApp.Repositories
{
    public class BirrasAppContext : DbContext
    {
        public BirrasAppContext(DbContextOptions<BirrasAppContext> options) : base(options) {}
        public BirrasAppContext() { }
        public DbSet<User> Users { get; set; }
        public DbSet<Meetup> Meetups { get; set; }
        public DbSet<UserMeetUp> UserMeetUps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMeetUp>().HasKey(um => new { um.UserId, um.MeetupId });

            modelBuilder.Entity<UserMeetUp>().HasOne(um => um.User);

            modelBuilder.Entity<UserMeetUp>()
                .HasOne<Meetup>(m => m.Meetup)
                .WithMany(um => um.UserMeetUps)
                .HasForeignKey( m => m.MeetupId);

            modelBuilder.Entity<UserRequestToMeetUp>().HasKey(request => new { request.UserId, request.MeetupId });

            modelBuilder.Entity<UserRequestToMeetUp>().HasOne(request => request.User);

            modelBuilder.Entity<UserRequestToMeetUp>()
                .HasOne(request => request.Meetup)
                .WithMany(um => um.Requests)
                .HasForeignKey(request => request.MeetupId);

            modelBuilder.Entity<User>().HasData(new
            {
                Id = 1,
                Username = "userAdmin", //Password: superpass123
                PasswordHash = StringToByteArray("0DA6971297717EF4DB27F961D00A61E7B4686C892993A1CC9404DAC64E2EC567B29B0D10DE43BD1A3E00C83A19E12E17A980A7597F7605AFD64BADA754100D34"),
                PasswordSalt = StringToByteArray("F8E3ED1716A2B5E8F59933FFBEA5CDF7BEF597605789FD6E5D109E907611E740A4AB317017726C55C2FC6D764BC78E539FD413224B2EEF97B49B762AF35BEB80E7489B3DD7A81C02F042C946B61F17F67B9B931A33FF02315AD1E83A86B697CADCFC612021907097826AF57C2CCBC0DB9A11B689876CA9DE264FB08A290BA359"),
                UserRol = UsersRoles.Admin,
                Email = "aosanchez@gmail.com"
            });

            modelBuilder.Entity<User>().HasData(new
            {
                Id = 2,
                Username = "user1", //Password: password1
                PasswordHash = StringToByteArray("6BA9CB2F653CB826B299C8F1F794F3E3EA28B5BA37D0937498B01AA311F07678211BCA7343812BAE3FC608BBA4D05265D3E13AF8848CBF54365D404BF34A60A1"),
                PasswordSalt = StringToByteArray("7DFBFACD602B32C0FC5FADF9E654A6BCC0381E3B9044FD831B2F0B07B1C2C728E41162DA0A488FD1DEC4E50A33DECDA355CDAEDB708ACF8A1125D3C47838F84DCB6035018740270124F5E763BC45710DD5CD08138144E2BD30526C188320BAD089CA3BFC783E4786ECBF79A8FE80C380CDC42BD7DE89DA257BCA76617D2655D9"),
                UserRol = UsersRoles.User,
                Email = "user1@santander.com"
            });

            modelBuilder.Entity<User>().HasData(new
            {
                Id = 3,
                Username = "user2", //Password: password2
                PasswordHash = StringToByteArray("3D54A46AD2EBF34BEAF67290AB3E21366327D1DA8B1EC4C2DCF8B8D9DC88FDC10F0493333F82FA3614BA8FC14F6F0BE618E609E8998AD1625CDB6DBBC62663E4"),
                PasswordSalt = StringToByteArray("7307344C704440B694F04227055AEA088C11198D589944B3ACD808D225E25AEE7AF3B106071105BC4B6FC5622108F621C33CFC9DC622347576DDC09B2F8CE429B5D3A9AD63248EA0F7E543A1687C97666021EA74848C469A96F129D0BC55E45152D7562464691F074786CA77956F61D6F2696C7C16E235835CED5430FBB83039"),
                UserRol = UsersRoles.User,
                Email = "user2@santander.com"
            });
        }

        private byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
