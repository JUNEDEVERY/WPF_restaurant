
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
    
public partial class Zakaz
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Zakaz()
    {

        this.ZakazIzMenu = new HashSet<ZakazIzMenu>();

    }


    public int idZakaz { get; set; }

    public int idStatus { get; set; }

    public int id_client { get; set; }

    public System.DateTime datetime { get; set; }



    public virtual Status Status { get; set; }

    public virtual Users Users { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ZakazIzMenu> ZakazIzMenu { get; set; }

}

}