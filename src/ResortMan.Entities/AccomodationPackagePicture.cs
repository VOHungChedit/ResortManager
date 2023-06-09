﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortMan.Entities;

public class AccomodationPackagePicture
{
    public int Id { get; set; }

    public byte[] Data { get; set; }

    public int AccomodationPackageId { get; set; }
    public AccomodationPackage AccomodationPackage { get; set; } = null!;

    public string ContentType { get; set; } = null!;
}
