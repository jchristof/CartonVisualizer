using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public class CubeiqContainer : MonoBehaviour {

    [XmlRoot(ElementName = "metadata")]
    public class Metadata {
        [XmlAttribute(AttributeName = "metadata1")]
        public string Metadata1 { get; set; }
        [XmlAttribute(AttributeName = "appstr")]
        public string Appstr { get; set; }
        [XmlAttribute(AttributeName = "getexedate")]
        public string Getexedate { get; set; }
        [XmlAttribute(AttributeName = "configpath")]
        public string Configpath { get; set; }
        [XmlAttribute(AttributeName = "aliaspath")]
        public string Aliaspath { get; set; }
        [XmlAttribute(AttributeName = "username")]
        public string Username { get; set; }
        [XmlAttribute(AttributeName = "fileversion")]
        public string Fileversion { get; set; }
        [XmlAttribute(AttributeName = "cleversion")]
        public string Cleversion { get; set; }
        [XmlAttribute(AttributeName = "companyname")]
        public string Companyname { get; set; }
        [XmlAttribute(AttributeName = "status")]
        public string Status { get; set; }
    }

    [XmlRoot(ElementName = "meta")]
    public class Meta {
        [XmlElement(ElementName = "metadata")]
        public Metadata Metadata { get; set; }
    }

    [XmlRoot(ElementName = "parameter")]
    public class Parameter {
        [XmlAttribute(AttributeName = "maxnumdecimals")]
        public string Maxnumdecimals { get; set; }
        [XmlAttribute(AttributeName = "fillupdensity")]
        public string Fillupdensity { get; set; }
        [XmlAttribute(AttributeName = "palletsalwaysstraightup")]
        public string Palletsalwaysstraightup { get; set; }
        [XmlAttribute(AttributeName = "sequencemeansdrop")]
        public string Sequencemeansdrop { get; set; }
        [XmlAttribute(AttributeName = "sequencemeanspriority")]
        public string Sequencemeanspriority { get; set; }
        [XmlAttribute(AttributeName = "usesequencewithingroups")]
        public string Usesequencewithingroups { get; set; }
        [XmlAttribute(AttributeName = "configpriorityisstage")]
        public string Configpriorityisstage { get; set; }
        [XmlAttribute(AttributeName = "combineposonlyinlaststage")]
        public string Combineposonlyinlaststage { get; set; }
        [XmlAttribute(AttributeName = "useonlylargestpossibleconfig")]
        public string Useonlylargestpossibleconfig { get; set; }
        [XmlAttribute(AttributeName = "groupnumberisprimarysequence")]
        public string Groupnumberisprimarysequence { get; set; }
    }

    [XmlRoot(ElementName = "parameters")]
    public class Parameters {
        [XmlElement(ElementName = "parameter")]
        public Parameter Parameter { get; set; }
    }

    [XmlRoot(ElementName = "container")]
    public class Container {
        [XmlAttribute(AttributeName = "containerid")]
        public string Containerid { get; set; }
        [XmlAttribute(AttributeName = "depth")]
        public string Depth { get; set; }
        [XmlAttribute(AttributeName = "width")]
        public string Width { get; set; }
        [XmlAttribute(AttributeName = "height")]
        public string Height { get; set; }
        [XmlAttribute(AttributeName = "maxweight")]
        public string Maxweight { get; set; }
        [XmlAttribute(AttributeName = "volume")]
        public string Volume { get; set; }
        [XmlAttribute(AttributeName = "settingsid")]
        public string Settingsid { get; set; }
        [XmlAttribute(AttributeName = "irregularinfrontview")]
        public string Irregularinfrontview { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "tareweight")]
        public string Tareweight { get; set; }
        [XmlAttribute(AttributeName = "bottomtotoploading")]
        public string Bottomtotoploading { get; set; }
        [XmlAttribute(AttributeName = "partialloadonfloor")]
        public string Partialloadonfloor { get; set; }
        [XmlAttribute(AttributeName = "defaultnum")]
        public string Defaultnum { get; set; }
        [XmlAttribute(AttributeName = "hardminfillpercentage")]
        public string Hardminfillpercentage { get; set; }
        [XmlAttribute(AttributeName = "dblengthunit")]
        public string Dblengthunit { get; set; }
        [XmlAttribute(AttributeName = "dbvolumeunit")]
        public string Dbvolumeunit { get; set; }
        [XmlAttribute(AttributeName = "dbweightunit")]
        public string Dbweightunit { get; set; }
        [XmlAttribute(AttributeName = "treatblankasmatchingcode")]
        public string Treatblankasmatchingcode { get; set; }
        [XmlAttribute(AttributeName = "showcentergravity")]
        public string Showcentergravity { get; set; }
        [XmlAttribute(AttributeName = "useonlyifrequired")]
        public string Useonlyifrequired { get; set; }
        [XmlAttribute(AttributeName = "noturnstage2")]
        public string Noturnstage2 { get; set; }
        [XmlAttribute(AttributeName = "uldwallsnotsupporting")]
        public string Uldwallsnotsupporting { get; set; }
        [XmlAttribute(AttributeName = "volumecapperc")]
        public string Volumecapperc { get; set; }
    }

    [XmlRoot(ElementName = "containers")]
    public class Containers {
        [XmlElement(ElementName = "container")]
        public List<Container> Container { get; set; }
    }

    [XmlRoot(ElementName = "setting")]
    public class Setting {
        [XmlAttribute(AttributeName = "settingsid")]
        public string Settingsid { get; set; }
        [XmlAttribute(AttributeName = "loadingmargin")]
        public string Loadingmargin { get; set; }
        [XmlAttribute(AttributeName = "maxruntime")]
        public string Maxruntime { get; set; }
        [XmlAttribute(AttributeName = "maxnonimproveiters")]
        public string Maxnonimproveiters { get; set; }
        [XmlAttribute(AttributeName = "minsupportrequired")]
        public string Minsupportrequired { get; set; }
        [XmlAttribute(AttributeName = "maxsupportheightdiff")]
        public string Maxsupportheightdiff { get; set; }
        [XmlAttribute(AttributeName = "estimatepercentage")]
        public string Estimatepercentage { get; set; }
        [XmlAttribute(AttributeName = "sequencemixok")]
        public string Sequencemixok { get; set; }
        [XmlAttribute(AttributeName = "seqseparatorthickness")]
        public string Seqseparatorthickness { get; set; }
        [XmlAttribute(AttributeName = "straightstackonly")]
        public string Straightstackonly { get; set; }
        [XmlAttribute(AttributeName = "keepitemsinonecontainer")]
        public string Keepitemsinonecontainer { get; set; }
        [XmlAttribute(AttributeName = "checkfeasibilityonly")]
        public string Checkfeasibilityonly { get; set; }
        [XmlAttribute(AttributeName = "neversplitboxes")]
        public string Neversplitboxes { get; set; }
        [XmlAttribute(AttributeName = "singlestackonly")]
        public string Singlestackonly { get; set; }
        [XmlAttribute(AttributeName = "dblengthunit")]
        public string Dblengthunit { get; set; }
        [XmlAttribute(AttributeName = "nocrossstacking")]
        public string Nocrossstacking { get; set; }
        [XmlAttribute(AttributeName = "maxspreadgap")]
        public string Maxspreadgap { get; set; }
        [XmlAttribute(AttributeName = "minbridgingsupport")]
        public string Minbridgingsupport { get; set; }
        [XmlAttribute(AttributeName = "stacksamecodeonly")]
        public string Stacksamecodeonly { get; set; }
        [XmlAttribute(AttributeName = "rollloadingrule")]
        public string Rollloadingrule { get; set; }
        [XmlAttribute(AttributeName = "unitlengthmargin")]
        public string Unitlengthmargin { get; set; }
        [XmlAttribute(AttributeName = "unitwidthmargin")]
        public string Unitwidthmargin { get; set; }
        [XmlAttribute(AttributeName = "unitheightmargin")]
        public string Unitheightmargin { get; set; }
        [XmlAttribute(AttributeName = "minunitspercontainer")]
        public string Minunitspercontainer { get; set; }
        [XmlAttribute(AttributeName = "unloadedunitstonextstage")]
        public string Unloadedunitstonextstage { get; set; }
        [XmlAttribute(AttributeName = "stacksamesequenceonly")]
        public string Stacksamesequenceonly { get; set; }
        [XmlAttribute(AttributeName = "singleunitblocks")]
        public string Singleunitblocks { get; set; }
        [XmlAttribute(AttributeName = "minunitsperblock")]
        public string Minunitsperblock { get; set; }
        [XmlAttribute(AttributeName = "loadproducttogether")]
        public string Loadproducttogether { get; set; }
        [XmlAttribute(AttributeName = "maxreach")]
        public string Maxreach { get; set; }
        [XmlAttribute(AttributeName = "loadproductsconsecutively")]
        public string Loadproductsconsecutively { get; set; }
        [XmlAttribute(AttributeName = "loadbynetweight")]
        public string Loadbynetweight { get; set; }
        [XmlAttribute(AttributeName = "productsuprightonly")]
        public string Productsuprightonly { get; set; }
        [XmlAttribute(AttributeName = "horizontalapproachdist")]
        public string Horizontalapproachdist { get; set; }
        [XmlAttribute(AttributeName = "verticalapproachdist")]
        public string Verticalapproachdist { get; set; }
        [XmlAttribute(AttributeName = "onesetpercontainer")]
        public string Onesetpercontainer { get; set; }
        [XmlAttribute(AttributeName = "pinwheelspreferred")]
        public string Pinwheelspreferred { get; set; }
        [XmlAttribute(AttributeName = "pinwheelsnotallowed")]
        public string Pinwheelsnotallowed { get; set; }
        [XmlAttribute(AttributeName = "oneseqperzone")]
        public string Oneseqperzone { get; set; }
        [XmlAttribute(AttributeName = "braceunitsatback")]
        public string Braceunitsatback { get; set; }
        [XmlAttribute(AttributeName = "combinesamecodeonly")]
        public string Combinesamecodeonly { get; set; }
        [XmlAttribute(AttributeName = "usetargetinfinalcontainer")]
        public string Usetargetinfinalcontainer { get; set; }
        [XmlAttribute(AttributeName = "usempe")]
        public string Usempe { get; set; }
    }

    [XmlRoot(ElementName = "settings")]
    public class Settings {
        [XmlElement(ElementName = "setting")]
        public Setting Setting { get; set; }
    }

    [XmlRoot(ElementName = "product")]
    public class Product {
        [XmlAttribute(AttributeName = "productid")]
        public string Productid { get; set; }
        [XmlAttribute(AttributeName = "length")]
        public string Length { get; set; }
        [XmlAttribute(AttributeName = "width")]
        public string Width { get; set; }
        [XmlAttribute(AttributeName = "height")]
        public string Height { get; set; }
        [XmlAttribute(AttributeName = "weight")]
        public string Weight { get; set; }
        [XmlAttribute(AttributeName = "turnable")]
        public string Turnable { get; set; }
        [XmlAttribute(AttributeName = "flatok")]
        public string Flatok { get; set; }
        [XmlAttribute(AttributeName = "sideupok")]
        public string Sideupok { get; set; }
        [XmlAttribute(AttributeName = "endupok")]
        public string Endupok { get; set; }
        [XmlAttribute(AttributeName = "bottomonly")]
        public string Bottomonly { get; set; }
        [XmlAttribute(AttributeName = "toponly")]
        public string Toponly { get; set; }
        [XmlAttribute(AttributeName = "deletewithsolution")]
        public string Deletewithsolution { get; set; }
        [XmlAttribute(AttributeName = "nofitems")]
        public string Nofitems { get; set; }
        [XmlAttribute(AttributeName = "nostickingout")]
        public string Nostickingout { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "loadonoutside")]
        public string Loadonoutside { get; set; }
        [XmlAttribute(AttributeName = "cost")]
        public string Cost { get; set; }
        [XmlAttribute(AttributeName = "volume")]
        public string Volume { get; set; }
        [XmlAttribute(AttributeName = "useconfigs")]
        public string Useconfigs { get; set; }
        [XmlAttribute(AttributeName = "useorientations")]
        public string Useorientations { get; set; }
        [XmlAttribute(AttributeName = "configunitisperc")]
        public string Configunitisperc { get; set; }
        [XmlAttribute(AttributeName = "upsidedown")]
        public string Upsidedown { get; set; }
        [XmlAttribute(AttributeName = "onright")]
        public string Onright { get; set; }
        [XmlAttribute(AttributeName = "onfront")]
        public string Onfront { get; set; }
        [XmlAttribute(AttributeName = "passthroughonly")]
        public string Passthroughonly { get; set; }
        [XmlAttribute(AttributeName = "color")]
        public string Color { get; set; }
        [XmlAttribute(AttributeName = "neverindoorway")]
        public string Neverindoorway { get; set; }
        [XmlAttribute(AttributeName = "dblengthunit")]
        public string Dblengthunit { get; set; }
        [XmlAttribute(AttributeName = "dbvolumeunit")]
        public string Dbvolumeunit { get; set; }
        [XmlAttribute(AttributeName = "dbweightunit")]
        public string Dbweightunit { get; set; }
        [XmlAttribute(AttributeName = "specialmarking")]
        public string Specialmarking { get; set; }
        [XmlAttribute(AttributeName = "notinoverhang")]
        public string Notinoverhang { get; set; }
        [XmlAttribute(AttributeName = "loadableinownbox")]
        public string Loadableinownbox { get; set; }
        [XmlAttribute(AttributeName = "drawaspallet")]
        public string Drawaspallet { get; set; }
        [XmlAttribute(AttributeName = "itemsareset")]
        public string Itemsareset { get; set; }
        [XmlAttribute(AttributeName = "created")]
        public string Created { get; set; }
        [XmlAttribute(AttributeName = "modified")]
        public string Modified { get; set; }
        [XmlAttribute(AttributeName = "supportsonlysamefootprint")]
        public string Supportsonlysamefootprint { get; set; }
        [XmlAttribute(AttributeName = "deletewithload")]
        public string Deletewithload { get; set; }
        [XmlAttribute(AttributeName = "qvalue")]
        public string Qvalue { get; set; }
        [XmlAttribute(AttributeName = "userboolean1")]
        public string Userboolean1 { get; set; }
        [XmlAttribute(AttributeName = "samefootprintbelow")]
        public string Samefootprintbelow { get; set; }
    }

    [XmlRoot(ElementName = "products")]
    public class Products {
        [XmlElement(ElementName = "product")]
        public List<Product> Product { get; set; }
    }

    [XmlRoot(ElementName = "config")]
    public class Config {
        [XmlAttribute(AttributeName = "productid")]
        public string Productid { get; set; }
        [XmlAttribute(AttributeName = "configindex")]
        public string Configindex { get; set; }
        [XmlAttribute(AttributeName = "auto")]
        public string Auto { get; set; }
        [XmlAttribute(AttributeName = "numunits")]
        public string Numunits { get; set; }
        [XmlAttribute(AttributeName = "length")]
        public string Length { get; set; }
        [XmlAttribute(AttributeName = "width")]
        public string Width { get; set; }
        [XmlAttribute(AttributeName = "height")]
        public string Height { get; set; }
        [XmlAttribute(AttributeName = "volume")]
        public string Volume { get; set; }
        [XmlAttribute(AttributeName = "weight")]
        public string Weight { get; set; }
        [XmlAttribute(AttributeName = "priority")]
        public string Priority { get; set; }
        [XmlAttribute(AttributeName = "turnable")]
        public string Turnable { get; set; }
        [XmlAttribute(AttributeName = "flatok")]
        public string Flatok { get; set; }
        [XmlAttribute(AttributeName = "onsideok")]
        public string Onsideok { get; set; }
        [XmlAttribute(AttributeName = "onendok")]
        public string Onendok { get; set; }
        [XmlAttribute(AttributeName = "bottomonly")]
        public string Bottomonly { get; set; }
        [XmlAttribute(AttributeName = "toponly")]
        public string Toponly { get; set; }
        [XmlAttribute(AttributeName = "splitafterloading")]
        public string Splitafterloading { get; set; }
        [XmlAttribute(AttributeName = "color")]
        public string Color { get; set; }
        [XmlAttribute(AttributeName = "clampspace")]
        public string Clampspace { get; set; }
        [XmlAttribute(AttributeName = "palletized")]
        public string Palletized { get; set; }
        [XmlAttribute(AttributeName = "indoorwayonly")]
        public string Indoorwayonly { get; set; }
        [XmlAttribute(AttributeName = "notonfloor")]
        public string Notonfloor { get; set; }
        [XmlAttribute(AttributeName = "singlestageuse")]
        public string Singlestageuse { get; set; }
        [XmlAttribute(AttributeName = "mirroring")]
        public string Mirroring { get; set; }
        [XmlAttribute(AttributeName = "passthroughonly")]
        public string Passthroughonly { get; set; }
        [XmlAttribute(AttributeName = "lengthandwidthswapped")]
        public string Lengthandwidthswapped { get; set; }
    }

    [XmlRoot(ElementName = "configs")]
    public class Configs {
        [XmlElement(ElementName = "config")]
        public Config Config { get; set; }
    }

    [XmlRoot(ElementName = "load")]
    public class Load {
        [XmlAttribute(AttributeName = "loadid")]
        public string Loadid { get; set; }
        [XmlAttribute(AttributeName = "currentlengthunit")]
        public string Currentlengthunit { get; set; }
        [XmlAttribute(AttributeName = "currentweightunit")]
        public string Currentweightunit { get; set; }
        [XmlAttribute(AttributeName = "currentvolumeunit")]
        public string Currentvolumeunit { get; set; }
        [XmlAttribute(AttributeName = "date")]
        public string Date { get; set; }
        [XmlAttribute(AttributeName = "stage")]
        public string Stage { get; set; }
        [XmlAttribute(AttributeName = "numstages")]
        public string Numstages { get; set; }
    }

    [XmlRoot(ElementName = "loads")]
    public class Loads {
        [XmlElement(ElementName = "load")]
        public Load Load { get; set; }
    }

    [XmlRoot(ElementName = "stage")]
    public class Stage {
        [XmlAttribute(AttributeName = "loadid")]
        public string Loadid { get; set; }
        [XmlAttribute(AttributeName = "stage")]
        public string _stage { get; set; }
        [XmlAttribute(AttributeName = "loadedvalue")]
        public string Loadedvalue { get; set; }
        [XmlAttribute(AttributeName = "onecontainerperseqno")]
        public string Onecontainerperseqno { get; set; }
        [XmlAttribute(AttributeName = "sequenceiscontainertype")]
        public string Sequenceiscontainertype { get; set; }
        [XmlAttribute(AttributeName = "mixedpalletload")]
        public string Mixedpalletload { get; set; }
        [XmlAttribute(AttributeName = "stablemixedpalletload")]
        public string Stablemixedpalletload { get; set; }
        [XmlAttribute(AttributeName = "numstagingpositions")]
        public string Numstagingpositions { get; set; }
        [XmlAttribute(AttributeName = "containerselectionrule")]
        public string Containerselectionrule { get; set; }
        [XmlAttribute(AttributeName = "usesequenceovercontainers")]
        public string Usesequenceovercontainers { get; set; }
        [XmlAttribute(AttributeName = "groupsplitting")]
        public string Groupsplitting { get; set; }
        [XmlAttribute(AttributeName = "optimizeloadedvalue")]
        public string Optimizeloadedvalue { get; set; }
        [XmlAttribute(AttributeName = "loadproportionally")]
        public string Loadproportionally { get; set; }
        [XmlAttribute(AttributeName = "loadfullstacksonly")]
        public string Loadfullstacksonly { get; set; }
        [XmlAttribute(AttributeName = "treatgroupsassets")]
        public string Treatgroupsassets { get; set; }
        [XmlAttribute(AttributeName = "sequencemeanspriority")]
        public string Sequencemeanspriority { get; set; }
        [XmlAttribute(AttributeName = "runtime")]
        public string Runtime { get; set; }
        [XmlAttribute(AttributeName = "sequencemaxlookahead")]
        public string Sequencemaxlookahead { get; set; }
    }

    [XmlRoot(ElementName = "stages")]
    public class Stages {
        [XmlElement(ElementName = "stage")]
        public Stage Stage { get; set; }
    }

    [XmlRoot(ElementName = "containertoload")]
    public class Containertoload {
        [XmlAttribute(AttributeName = "loadid")]
        public string Loadid { get; set; }
        [XmlAttribute(AttributeName = "containerid")]
        public string Containerid { get; set; }
        [XmlAttribute(AttributeName = "containernum")]
        public string Containernum { get; set; }
        [XmlAttribute(AttributeName = "sequence")]
        public string Sequence { get; set; }
        [XmlAttribute(AttributeName = "numused")]
        public string Numused { get; set; }
        [XmlAttribute(AttributeName = "numlocked")]
        public string Numlocked { get; set; }
        [XmlAttribute(AttributeName = "auto")]
        public string Auto { get; set; }
        [XmlAttribute(AttributeName = "stage")]
        public string Stage { get; set; }
    }

    [XmlRoot(ElementName = "containerstoload")]
    public class Containerstoload {
        [XmlElement(ElementName = "containertoload")]
        public List<Containertoload> Containertoload { get; set; }
    }

    [XmlRoot(ElementName = "producttoload")]
    public class Producttoload {
        [XmlAttribute(AttributeName = "loadid")]
        public string Loadid { get; set; }
        [XmlAttribute(AttributeName = "auto")]
        public string Auto { get; set; }
        [XmlAttribute(AttributeName = "productid")]
        public string Productid { get; set; }
        [XmlAttribute(AttributeName = "stage")]
        public string Stage { get; set; }
        [XmlAttribute(AttributeName = "batch")]
        public string Batch { get; set; }
        [XmlAttribute(AttributeName = "autoid")]
        public string Autoid { get; set; }
        [XmlAttribute(AttributeName = "quantity")]
        public string Quantity { get; set; }
        [XmlAttribute(AttributeName = "numloaded")]
        public string Numloaded { get; set; }
        [XmlAttribute(AttributeName = "frompreviousstage")]
        public string Frompreviousstage { get; set; }
    }

    [XmlRoot(ElementName = "productstoload")]
    public class Productstoload {
        [XmlElement(ElementName = "producttoload")]
        public List<Producttoload> Producttoload { get; set; }
    }

    [XmlRoot(ElementName = "loadedcontainer")]
    public class Loadedcontainer {
        [XmlAttribute(AttributeName = "loadid")]
        public string Loadid { get; set; }
        [XmlAttribute(AttributeName = "seqno")]
        public string Seqno { get; set; }
        [XmlAttribute(AttributeName = "containerid")]
        public string Containerid { get; set; }
        [XmlAttribute(AttributeName = "volumeutilization")]
        public string Volumeutilization { get; set; }
        [XmlAttribute(AttributeName = "weightutilization")]
        public string Weightutilization { get; set; }
        [XmlAttribute(AttributeName = "loadedvolume")]
        public string Loadedvolume { get; set; }
        [XmlAttribute(AttributeName = "loadedweight")]
        public string Loadedweight { get; set; }
        [XmlAttribute(AttributeName = "loadedvalue")]
        public string Loadedvalue { get; set; }
        [XmlAttribute(AttributeName = "loadeditems")]
        public string Loadeditems { get; set; }
        [XmlAttribute(AttributeName = "numunits")]
        public string Numunits { get; set; }
        [XmlAttribute(AttributeName = "numblocks")]
        public string Numblocks { get; set; }
        [XmlAttribute(AttributeName = "numproducts")]
        public string Numproducts { get; set; }
        [XmlAttribute(AttributeName = "numfixed")]
        public string Numfixed { get; set; }
        [XmlAttribute(AttributeName = "lengthgravitycentreperc")]
        public string Lengthgravitycentreperc { get; set; }
        [XmlAttribute(AttributeName = "widthgravitycentreperc")]
        public string Widthgravitycentreperc { get; set; }
        [XmlAttribute(AttributeName = "heightgravitycentreperc")]
        public string Heightgravitycentreperc { get; set; }
        [XmlAttribute(AttributeName = "maxlength")]
        public string Maxlength { get; set; }
        [XmlAttribute(AttributeName = "maxwidth")]
        public string Maxwidth { get; set; }
        [XmlAttribute(AttributeName = "maxheight")]
        public string Maxheight { get; set; }
        [XmlAttribute(AttributeName = "locked")]
        public string Locked { get; set; }
        [XmlAttribute(AttributeName = "warningcode")]
        public string Warningcode { get; set; }
        [XmlAttribute(AttributeName = "frequency")]
        public string Frequency { get; set; }
        [XmlAttribute(AttributeName = "stage")]
        public string Stage { get; set; }
    }

    [XmlRoot(ElementName = "loadedcontainers")]
    public class Loadedcontainers {
        [XmlElement(ElementName = "loadedcontainer")]
        public Loadedcontainer Loadedcontainer { get; set; }
    }

    [XmlRoot(ElementName = "block")]
    public class Block {
        [XmlAttribute(AttributeName = "loadid")]
        public string Loadid { get; set; }
        [XmlAttribute(AttributeName = "block")]
        public string _block { get; set; }
        [XmlAttribute(AttributeName = "productid")]
        public string Productid { get; set; }
        [XmlAttribute(AttributeName = "numdeep")]
        public string Numdeep { get; set; }
        [XmlAttribute(AttributeName = "numwide")]
        public string Numwide { get; set; }
        [XmlAttribute(AttributeName = "numhigh")]
        public string Numhigh { get; set; }
        [XmlAttribute(AttributeName = "length")]
        public string Length { get; set; }
        [XmlAttribute(AttributeName = "width")]
        public string Width { get; set; }
        [XmlAttribute(AttributeName = "height")]
        public string Height { get; set; }
        [XmlAttribute(AttributeName = "depthcoord")]
        public string Depthcoord { get; set; }
        [XmlAttribute(AttributeName = "widthcoord")]
        public string Widthcoord { get; set; }
        [XmlAttribute(AttributeName = "heightcoord")]
        public string Heightcoord { get; set; }
        [XmlAttribute(AttributeName = "lengthspread")]
        public string Lengthspread { get; set; }
        [XmlAttribute(AttributeName = "widthspread")]
        public string Widthspread { get; set; }
        [XmlAttribute(AttributeName = "orientationindex")]
        public string Orientationindex { get; set; }
        [XmlAttribute(AttributeName = "autoid")]
        public string Autoid { get; set; }
        [XmlAttribute(AttributeName = "rotation")]
        public string Rotation { get; set; }
        [XmlAttribute(AttributeName = "quantity")]
        public string Quantity { get; set; }
        [XmlAttribute(AttributeName = "weight")]
        public string Weight { get; set; }
        [XmlAttribute(AttributeName = "volume")]
        public string Volume { get; set; }
        [XmlAttribute(AttributeName = "unitlength")]
        public string Unitlength { get; set; }
        [XmlAttribute(AttributeName = "unitwidth")]
        public string Unitwidth { get; set; }
        [XmlAttribute(AttributeName = "unitheight")]
        public string Unitheight { get; set; }
        [XmlAttribute(AttributeName = "cnt")]
        public string Cnt { get; set; }
        [XmlAttribute(AttributeName = "stage")]
        public string Stage { get; set; }
        [XmlAttribute(AttributeName = "containerseqno")]
        public string Containerseqno { get; set; }
        [XmlAttribute(AttributeName = "lowestheightcrdleft")]
        public string Lowestheightcrdleft { get; set; }
        [XmlAttribute(AttributeName = "layer")]
        public string Layer { get; set; }
        [XmlAttribute(AttributeName = "fixed")]
        public string Fixed { get; set; }
    }

    [XmlRoot(ElementName = "blocks")]
    public class Blocks {
        [XmlElement(ElementName = "block")]
        public List<Block> Block { get; set; }
    }

    [XmlRoot(ElementName = "cubeiq")]
    public class Cubeiq {
        [XmlElement(ElementName = "meta")]
        public Meta Meta { get; set; }
        [XmlElement(ElementName = "parameters")]
        public Parameters Parameters { get; set; }
        [XmlElement(ElementName = "containers")]
        public Containers Containers { get; set; }
        [XmlElement(ElementName = "ceilingheights")]
        public string Ceilingheights { get; set; }
        [XmlElement(ElementName = "floorheights")]
        public string Floorheights { get; set; }
        [XmlElement(ElementName = "lostspaces")]
        public string Lostspaces { get; set; }
        [XmlElement(ElementName = "zones")]
        public string Zones { get; set; }
        [XmlElement(ElementName = "axles")]
        public string Axles { get; set; }
        [XmlElement(ElementName = "cdrawboxes")]
        public string Cdrawboxes { get; set; }
        [XmlElement(ElementName = "settings")]
        public Settings Settings { get; set; }
        [XmlElement(ElementName = "products")]
        public Products Products { get; set; }
        [XmlElement(ElementName = "orientations")]
        public string Orientations { get; set; }
        [XmlElement(ElementName = "configs")]
        public Configs Configs { get; set; }
        [XmlElement(ElementName = "items")]
        public string Items { get; set; }
        [XmlElement(ElementName = "pdrawboxes")]
        public string Pdrawboxes { get; set; }
        [XmlElement(ElementName = "loads")]
        public Loads Loads { get; set; }
        [XmlElement(ElementName = "stages")]
        public Stages Stages { get; set; }
        [XmlElement(ElementName = "containerstoload")]
        public Containerstoload Containerstoload { get; set; }
        [XmlElement(ElementName = "productstoload")]
        public Productstoload Productstoload { get; set; }
        [XmlElement(ElementName = "loadedcontainers")]
        public Loadedcontainers Loadedcontainers { get; set; }
        [XmlElement(ElementName = "blocks")]
        public Blocks Blocks { get; set; }
    }
}
