using System;
using Microsoft.EntityFrameworkCore;

namespace Shop.API.Data;

public class ApplicationDBContext(
    DbContextOptions<ApplicationDBContext> options
) : DbContext(options)
{

}
