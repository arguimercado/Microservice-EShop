global using Microsoft.EntityFrameworkCore;
global using MediatR;

global using Order.Domain.SalesOrderDomain.Models;
global using Order.Domain.CustomerDomain.Models;
global using Order.Domain.ProductDomain.Models;

global using Order.Domain.SalesOrderDomain.Types;
global using Order.Domain.CustomerDomain.Types;
global using Order.Domain.ProductDomain.Types;

global using Order.Infrastructure.Persistence;
global using Order.Domain.Commons.Shared.ValueObjects;

global using Order.Domain.Commons.Abstractions;
global using Order.Infrastructure.Persistence.Extensions;


global using Order.Infrastructure.Persistence.Interceptors;