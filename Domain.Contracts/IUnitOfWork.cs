namespace Domain.Contracts;

public interface IUnitOfWork
{
    ICompanyRepository CompanyRepository { get; }

    Task CompleteASync();
}