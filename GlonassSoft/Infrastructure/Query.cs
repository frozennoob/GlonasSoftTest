using System;

namespace GlonassSoft.Infrastructure
{
    public class Query
    {
        public Guid QueryId { get; set; }
        public Guid UserId { get; set; }
        public byte Percent { get; set; }
        public int CountSignIn { get; set; }
    }
}
