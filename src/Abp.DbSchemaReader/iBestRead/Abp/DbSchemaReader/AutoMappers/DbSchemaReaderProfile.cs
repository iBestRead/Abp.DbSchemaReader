using AutoMapper;
using iBestRead.Abp.DbSchemaReader.Dtos;
using iBestRead.Abp.DbSchemaReader.Entities;

namespace iBestRead.Abp.DbSchemaReader.AutoMappers
{
    public class DbSchemaReaderProfile : Profile
    {
        public DbSchemaReaderProfile()
        {
            CreateMap<Column, ColumnDto>();
            CreateMap<Table, TableDto>();
        }
    }
}