namespace Beauty.Web.Areas.Identity.Data
{
    public interface ISeedIdentity
    {
        void EnsurePopulated(IApplicationBuilder app);
    }
}
