﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudos.Domain.Entities
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Estado Estado { get; set; }
    }
}
