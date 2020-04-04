using System.Threading.Tasks;
using Discord.Commands;

namespace VeryCoolBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("Commands")]
        [Alias("commands")]

        public async Task CommandsList()
        {
            await ReplyAsync("All commands should be prefixed with a ! \r\n The list of available commands are:\r\n ``` Fox \r\n Boose \r\n Hatcha \r\n Blackthorn \r\n MMDT ``` \r\n ");
        }

        [Command("Fox")]
        public async Task Fox()
        {
            await ReplyAsync(":fox: what a skinny fuck");
        }

        [Command("Boose")]
        public async Task Boose()
        {
            await ReplyAsync(":gorilla: well well well son, i went to the bakers and got 6 rolls, 3 pasties, and 2 pies. there's a can of coke on the counter for you, and a mars bar. don't eat it all at once");
        }

        [Command("Hatcha")]
        public async Task Hatcha()
        {
            await ReplyAsync(":angel_tone5: hello saint hatcher i hope you're having a good day");
        }

        [Command("MMDT")]
        [Alias("Wh1","wh1","wh1ttle","Wh1ttle")]
        public async Task MMDT()
        {
            await ReplyAsync("Just leave when you've lost");
        }

        [Command("Blackthorn")]
        [Alias("ssgta","Wit","Dese")]
        public async Task Ssgta()
        {
            await ReplyAsync("Did you mean God?");
        }
    }
}
