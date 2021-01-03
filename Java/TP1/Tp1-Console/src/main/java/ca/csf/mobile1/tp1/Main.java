package ca.csf.mobile1.tp1;

import ca.csf.mobile1.tp1.chemical.compound.ChemicalCompoundFactory;
import ca.csf.mobile1.tp1.chemical.element.ChemicalElementFactory;
import ca.csf.mobile1.tp1.chemical.element.ChemicalElementRepository;
import ca.csf.mobile1.tp1.controler.ChemicalControler;
import ca.csf.mobile1.tp1.view.ChemicalCompoundCalculatorConsoleView;

public class Main {

    public static void main(String[] args) throws Exception {
        //TODO : Instancier le modèle
        ChemicalElementRepository chemicalElementRepository = createChemicalElementRepository();
        //ChemicalCompoundFactory chemicalCompoundFactory = new ChemicalCompoundFactory(chemicalElementRepository);
        //TODO : Instancier la vue (ChemicalCompoundCalculatorConsoleView, qui doit être complétée)
        ChemicalCompoundCalculatorConsoleView chemicalCompoundCalculatorConsoleView = new ChemicalCompoundCalculatorConsoleView();
        //TODO : Instancier le contrôleur
        ChemicalControler chemicalControler = new ChemicalControler(chemicalElementRepository, chemicalCompoundCalculatorConsoleView);
        chemicalControler.view();

    }

    private static ChemicalElementRepository createChemicalElementRepository() throws Exception {
        ChemicalElementFactory chemicalElementFactory = new ChemicalElementFactory();
        ChemicalElementRepository chemicalElementRepository = new ChemicalElementRepository();
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Hydrogene,H,1,1.00794"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Helium,He,2,4.002602"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Lithium,Li,3,6.941"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Beryllium,Be,4,9.012182"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Bore,B,5,10.811"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Carbone,C,6,12.0107"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Azote,N,7,14.00674"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Oxygene,O,8,15.9994"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Fluor,F,9,18.9984032"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Neon,Ne,10,20.1797"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Sodium,Na,11,22.98976928"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Magnesium,Mg,12,24.3050"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Aluminium,Al,13,26.9815386"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Silicium,Si,14,28.0855"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Phosphore,P,15,30.973762"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Soufre,S,16,32.066"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Chlore,Cl,17,35.4527"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Argon,Ar,18,39.948"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Potassium,K,19,39.0983"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Calcium,Ca,20,40.078"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Scandium,Sc,21,44.955912"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Titane ,Ti,22,47.867"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Vanadium,V,23,50.9415"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Chrome,Cr,24,51.9961"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Manganese,Mn,25,54.938045"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Fer,Fe,26,55.845"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Cobalt,Co,27,58.933195"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Nickel,Ni,28,58.6934"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Cuivre,Cu,29,63.546"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Zinc,Zn,30,65.39"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Gallium,Ga,31,69.723"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Germanium,Ge,32,72.61"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Arsenic,As,33,74.92160"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Selenium,Se,34,78.96"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Brome,Br,35,79.904"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Krypton,Kr,36,83.80"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Rubidium,Rb,37,85.4678"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Strontium,Sr,38,87.62"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Yttrium,Y,39,88.90585"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Zirconium,Zr,40,91.224"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Niobium,Nb,41,92.90638"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Molybdene,Mo,42,95.94"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Technetium,Tc,43,97.9072"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Ruthenium,Ru,44,101.07"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Rhodium,Rh,45,102.90550"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Palladium,Pd,46,106.42"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Argent,Ag,47,107.8682"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Cadmium,Cd,48,112.411"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Indium,In,49,114.818"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Etain,Sn,50,118.710"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Antimoine,Sb,51,121.760"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Tellure,Te,52,127.60"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Iode,I,53,126.90447"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Xenon,Xe,54,131.29"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Caesium,Cs,55,132.9054519"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Baryum,Ba,56,137.327"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Lanthane,La,57,138.90547"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Cerium,Ce,58,140.116"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Praseodyme,Pr,59,140.90765"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Neodyme,Nd,60,144.242"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Promethium,Pm,61,144.9127"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Samarium,Sm,62,150.36"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Europium,Eu,63,151.964"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Gadolium,Gd,64,157.25"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Terbium,Tb,65,158.92535"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Dysprosium,Dy,66,162.500"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Holmium,Ho,67,164.93032"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Erbium,Er,68,167.259"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Thullium,Tm,69,168.93421"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Ytterbium,Yb,70,173.04"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Lutecium,Lu,71,174.967"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Hafnium,Hf,72,178.49"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Tantale,Ta,73,180.94788"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Tungstene,W,74,183.84"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Rhenium,Re,75,186.207"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Osmium,Os,76,190.23"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Iridium,Ir,77,192.217"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Platine ,Pt,78,195.084"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Or,Au,79,196.966569"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Mercure,Hg,80,200.59"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Thallium,Tl,81,204.3833"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Plomb,Pb,82,207.2"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Bismuth,Bi,83,208.98040"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Polonium ,Po,84,208.9824"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Astate,At,85,209.9871"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Radon,Rn,86,222.0176"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Francium,Fr,87,223.0197"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Radium,Ra,88,226.0254"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Actinium,Ac,89,227.0277"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Thorium,Th,90,232.03806"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Protactinium,Pa,91,231.03588"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Uranium,U,92,238.02891"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Neptunium,Np,93,237.0482"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Plutonium,Pu,94,244.0642"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Americium,Am,95,243.0614"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Curium,Cm,96,247.0703"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Berkelium,Bk,97,247.0703"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Californium,Cf,98,251.0796"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Einsteinium,Es,99,252.0830"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Fermium,Fm,100,257.0951"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Mendelevium,Md,101,258.0984"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Nobelium,No,102,259.1011"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Lawrencium,Lr,103,262.110"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Rutherfordium,Rf,104,267.1125"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Dubnium,Db,105,268.1144"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Seaborgium,Sg,106,271.1219"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Bohrium,Bh,107,272.1247"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Hassium,Hs,108,277.1341"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Meitnerium,Mt,109,276.1388"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Darmstadtium,Ds,110,281.1463"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Roentgenium,Rg,111,280.1535"));
        chemicalElementRepository.add(chemicalElementFactory.createFromString("Copernicium,Cn,112,285.0"));
        return chemicalElementRepository;
    }

}
