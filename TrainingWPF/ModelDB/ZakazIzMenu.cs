
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace TrainingWPF.ModelDB
{

using System;
    using System.Collections.Generic;
    
public partial class ZakazIzMenu
{

    public int idZakazMenu { get; set; }

    public int idMenu { get; set; }

    public int idNapitok { get; set; }

    public int idzakaz { get; set; }

    public int quantity { get; set; }



    public virtual Menu Menu { get; set; }

    public virtual Napitok Napitok { get; set; }

    public virtual Zakaz Zakaz { get; set; }

}

}
