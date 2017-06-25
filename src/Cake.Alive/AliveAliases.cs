using Cake.Core.Annotations;

namespace Cake.Alive
{
    /// <summary>
    /// Contains functionality for checking if stuff is alive
    /// </summary>
    [CakeAliasCategory("Alive")]
    public static class AliveAliases
    {
        [CakeMethodAlias]
        public static void HttpPing(this Core.ICakeContext context, string url)
        {
            using (var alive = new Alive(context.Log))
            {
                alive.HttpPing(url, new AliveSettings());
            }
        }

        [CakeMethodAlias]
        public static void HttpPing(this Core.ICakeContext context, string url, AliveSettings settings)
        {
            using (var alive = new Alive(context.Log))
            {
                alive.HttpPing(url, settings);
            }
        }
    }
}