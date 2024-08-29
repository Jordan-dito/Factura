using Api_Factura.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Factura> Facturas { get; set; }
    public DbSet<DetalleFactura> DetalleFacturas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de la entidad Cliente
        modelBuilder.Entity<Cliente>()
            .HasKey(c => c.IdCliente);

        modelBuilder.Entity<Cliente>()
            .Property(c => c.Identificacion)
            .IsRequired()
            .HasMaxLength(20);

        modelBuilder.Entity<Cliente>()
            .Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        // Configuración de la entidad Producto
        modelBuilder.Entity<Producto>()
            .HasKey(p => p.IdProducto);

        modelBuilder.Entity<Producto>()
            .Property(p => p.Codigo)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Producto>()
            .Property(p => p.Descripcion)
            .HasMaxLength(200);

        modelBuilder.Entity<Producto>()
            .Property(p => p.Categoria)
            .HasMaxLength(100);

        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // Configuración de la entidad Factura
        modelBuilder.Entity<Factura>()
            .HasKey(f => f.IdFactura);

        modelBuilder.Entity<Factura>()
            .Property(f => f.Establecimiento)
            .IsRequired()
            .HasMaxLength(10);

        modelBuilder.Entity<Factura>()
            .Property(f => f.PuntoEmision)
            .IsRequired()
            .HasMaxLength(10);

        modelBuilder.Entity<Factura>()
            .Property(f => f.NumeroFactura)
            .IsRequired()
            .HasMaxLength(10);

        modelBuilder.Entity<Factura>()
            .Property(f => f.Fecha)
            .IsRequired()
            .HasColumnType("date");

        modelBuilder.Entity<Factura>()
            .HasOne<Cliente>(f => f.Cliente)
            .WithMany(c => c.Facturas)
            .HasForeignKey(f => f.IdCliente)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Factura>()
            .HasMany(f => f.Detalles)
            .WithOne(d => d.Factura)
            .HasForeignKey(d => d.IdFactura)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuración de la entidad DetalleFactura
        modelBuilder.Entity<DetalleFactura>()
            .HasKey(d => d.IdDetalle);

        modelBuilder.Entity<DetalleFactura>()
            .Property(d => d.Cantidad)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        modelBuilder.Entity<DetalleFactura>()
            .Property(d => d.Precio)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        modelBuilder.Entity<DetalleFactura>()
            .Property(d => d.IVA)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        modelBuilder.Entity<DetalleFactura>()
            .Property(d => d.Subtotal)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        modelBuilder.Entity<DetalleFactura>()
            .HasOne<Producto>(d => d.Producto)
            .WithMany(p => p.Detalles)
            .HasForeignKey(d => d.IdProducto)
            .OnDelete(DeleteBehavior.Restrict);
    }


}
