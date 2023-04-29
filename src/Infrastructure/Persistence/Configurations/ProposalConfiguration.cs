using AnyTime.Core.Domain.Modules.Proposals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnyTime.Infrastructure.Persistence.Configurations;

public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
{
  public void Configure(EntityTypeBuilder<Proposal> builder)
  {
    builder.HasOne(proposal => proposal.announcement);
  }
}
