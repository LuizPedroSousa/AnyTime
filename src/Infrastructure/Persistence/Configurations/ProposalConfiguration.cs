using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnyTime.Infrastructure.Persistence.Configurations;

using AnyTime.Core.Domain.Modules.Jobs;

public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
{
  public void Configure(EntityTypeBuilder<Proposal> builder)
  {
    builder.HasOne(proposal => proposal.announcement);
  }
}
