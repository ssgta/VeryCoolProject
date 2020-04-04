using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace VeryCoolBot.Admin
{
    public static class BannedWords
    {

        public static async Task<bool> CheckForBannedWords(string messageContent)
        {
            try
            {
                bool messageContainsBadWord = false;

                foreach (var badWord in bannedWordsArray)
                {
                    if (messageContent.ToLower().Contains(badWord.ToLower()))
                    {
                        messageContainsBadWord = true;
                        break;
                    }
                }

                return messageContainsBadWord;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static async Task PunishBannedWord(SocketCommandContext context)
        {
            await context.Channel.SendMessageAsync($"That's not a very pleasant way to behave, is it, {context.Message.Author.Mention}? Buck up your ideas, buddy boy.");

        }
        static readonly string[] bannedWordsArray = new string[]
            {
                " nig ",
                "nigger",
                "nıgger",
                "NlGGER",
                "nígger",
                "Nïgger",
                "f@g",
                "ńigger",
                "ńigger",
                "fåggot",
                "n1gger",
                "nıgg3r",
                "nıgg3r",
                "faggot",
                "nigga",
                "fag",
                "kneeger",
                "niggers",
                "niggerz",
                "kneegers",
                "niqqa",
                "n_i_g_g_a",
                "n.i.g.g.a",
                "n,i,g,g,a",
                "n-i-g-g-a",
                "n=i=g=g=a",
                "n+i+g+g+a",
                "nigga",
                "n/i/g/g/a",
                "kike",
                "coon",
                "gook",
                "chink",
                "test123",
          };
    }
}