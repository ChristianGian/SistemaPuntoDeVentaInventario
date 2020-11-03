﻿using CapaDatos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Contracts
{
    public interface IOrdenCanceladaRepository
    {
        List<OrdenCancelada> Read(DateTime fechaIncio, DateTime fechaFin);
        int InsertarOrdenCancelada(OrdenCancelada entidad);
    }
}
