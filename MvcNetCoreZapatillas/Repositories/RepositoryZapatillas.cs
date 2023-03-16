using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreZapatillas.Data;
using MvcNetCoreZapatillas.Models;
using System.Diagnostics.Metrics;

#region SQL SERVER
//VUESTRO PROCEDIMIENTO DE PAGINACION DE IMAGENES DE ZAPATILLAS
#endregion

namespace MvcNetCoreZapatillas.Repositories
{
    #region
    //    ALTER VIEW V_GRUPO_IMAGENS
    //AS
    //        SELECT CAST(
    //            ROW_NUMBER() OVER (ORDER BY IMAGENESZAPASPRACTICA.IDPRODUCTO) AS INT) AS POSICION
    //            , ISNULL(IDIMAGEN, 0) AS IDIMAGEN, IMAGENESZAPASPRACTICA.IDPRODUCTO, IMAGEN,IMAGENESZAPASPRACTICA.IDPRODUCTO
    //            POSID FROM ZAPASPRACTICA INNER JOIN IMAGENESZAPASPRACTICA ON IMAGENESZAPASPRACTICA.IDPRODUCTO=ZAPASPRACTICA.IDPRODUCTO
    //    GO

    //alter PROCEDURE SP_GRUPO_IMAGENES
    //(@POSICION INT, @POSICIOID INT)
    //AS
    //    SELECT IDIMAGEN,POSID, IDPRODUCTO, IMAGEN
    //    FROM V_GRUPO_IMAGENS
    //    WHERE POSICION >= @POSICION AND POSICION<(@POSICION + 1) AND  POSID=@POSICIOID
    //GO
    #endregion
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        public List<Zapatilla> GetZapatillas()
        {
            var consulta = from datos in this.context.Zapatillas
                           select datos;
            return consulta.ToList();
        }

        public Zapatilla GetZapatillasDetalles(int id)
        {
            var consulta = from datos in this.context.Zapatillas
                           where datos.IdProducto == id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public async Task<List<ImagenZapatilla>> GetGrupoImagenesAsync(int posicion, int id)
        {
            string sql = "SP_GRUPO_IMAGENES @POSICION,@POSID";
            SqlParameter pamposicion =
                new SqlParameter("@POSICION", posicion);
            SqlParameter pamid =
                new SqlParameter("@POSID", id);
            var consulta =
                this.context.ImagenesZapatillas.FromSqlRaw(sql, pamposicion, pamid);
            return await consulta.ToListAsync();
        }

        public int GetNumeroImagens()
        {
            return this.context.ImagenesZapatillas.Count();
        }
    }
}
