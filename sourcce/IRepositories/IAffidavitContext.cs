namespace IRepositories
{
    using Entities;
    using System.Data.Entity;

    public interface IAffidavitContext
    {
        DbSet<NS_tblCampaign> NS_tblCampaign { get; set; }
        DbSet<NS_tblProduct> NS_tblProduct { get; set; }
        DbSet<NS_tblProductFamily> NS_tblProductFamily { get; set; }
    }
}
