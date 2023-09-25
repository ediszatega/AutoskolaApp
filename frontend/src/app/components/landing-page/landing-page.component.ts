import { Component } from '@angular/core';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css'],
})
export class LandingPageComponent {
  openPhase: number | null = null; // Track the currently open phase
  openQuestion: number | null = null; // Track the currently open question

  phases = [
    {
      number: 1,
      moreLessLabel: 'Prikaži više',
      moreLess: false,
      instructionTitle: 'Ljekarsko uvjerenje',
      instructionDescription:
        'Zdravstvene preglede kandidata za vozača i vozača motornog vozila obavljaju zdravstvene ustanove koje su za to ovlaštene u skladu sa zakonima. U Konjicu je to Dom Zdravlja. U slučaju nekih nejasnoća do ljekarskog uvjerenja ćete doći najbrže i najlakše u dogovoru sa osobljem autoškole "A&S".',
      additionalInstructionDescription:
        'Ukoliko je potrebno, naše osoblje će Vas odvesti do zdravstvene ustanove, biti sa Vama i vratiti Vas ponovo tamo gdje Vama odgovara. Ljekarsko uvjerenje sadrži 4 pregleda: oftamolog, neuropsihijatar, psiholog, medicina rada (pregled i zaključak)',
    },
    {
      number: 2,
      moreLessLabel: 'Prikaži više',
      moreLess: false,
      instructionTitle: 'Teoretska obuka u kabinetu autoškole "A&S"',
      instructionDescription:
        'U autoškoli "A&S" teoretsku nastavu možete pohađati svakog radnog dana u prijepodnevnim i popodnevnim terminima po izboru. Broj časova zavisi od kategorije za koji kandidat polaže. U toku jednog dana kandidat može imati najviše četri časa. Teoretska nastava se održava u prostorijama autoškole "A&S" uz korištenje adekvatne opreme i literature.',
      additionalInstructionDescription:
        'Kabinet se nalazi u ulici Maršala Tita bb (SC Partizanovo). Predavanja održava licencirani predavač teoretske nastave MA.Sanida Prevljak diplomirani inžinjer saobraćaja i komunikacija. Na časovima teoretske nastave savladaćete kompletno gradivo i biti spremni za uspješno polaganje testova iz prvog pokušaja.',
    },
    {
      number: 3,
      moreLessLabel: 'Prikaži više',
      moreLess: false,
      instructionTitle: 'Predavanje iz Osnova pružanja prve pomoći"',
      instructionDescription:
        'Predavanje iz Osnova pružanja prve pomoći održava se u prostorijama Crvenog križa općine Konjic, Kolonija bb.',
    },
    {
      number: 4,
      moreLessLabel: 'Prikaži više',
      moreLess: false,
      instructionTitle:
        'Polaganje Ispita poznavanja propisa o sigurnosti saobraćaja na putevima (testovi) i polaganje Ispita iz osnova pružanja prve pomoći',
      instructionDescription:
        'Polaganje teoretskog djela vozačkog ispita se obavlja nakon završene edukacije o poznavanju saobraćajnih propisa. Polaganje prve pomoći se obavlja nakon završene edukacije iz osnova prve pomoći. Polaganje testova i prve pomoći se vrši pred komisijom koju organizuje Ministarstvo obrazovanja HNK.',
      additionalInstructionDescription:
        'Naš predavač će za vas izvršiti kompletan proces prijave za polaganje, te vas obavjestiti o terminu polaganja. Polaganje teoretskog djela vozačkog ispita i prve pomoći u Konjicu održava se u prostorijama JU "Srednja škola Konjic", dva puta mjesečno.',
    },
    {
      number: 5,
      moreLessLabel: 'Prikaži više',
      moreLess: false,
      instructionTitle:
        'Obuka upravljanja motornim vozilom (praktični dio) i polaganje ispita iz upravljanja motornim vozilom',
      instructionDescription:
        'Nakon položenog ispita teoretskog djela i prve pomoći kandidat može početi sa procesom obuke upravljanja motornim vozilom u dogovoru sa instruktorom. Način, raspored i termini obuke na vozilu se prilagođavaju individalno svakom kandidatu.',
      additionalInstructionDescription:
        'Broj časova obuke upravljanja motornim vozilom zavisi od kategorije koju polažete. Nakon uspješno završene obuke na vozilu kandidat izlazi na polaganje praktičnog dijela obuke iz poznavanja upravljanja vozilom. Polaganje se vrši pred komisijom koju organizuje Ministarstvo obrazovanja.',
    },
  ];

  reviews = [
    {
      img: '../../../assets/review1.jpg',
      name: 'Mersad Graho',
      text: 'Veliko hvala autoškoli "A&S" koja nam je omogućila sve potrebne uvjete za sitcanje znanja koje ćemo svakako nastaviti da primjenjujemo dalje u praktičnim uvjetima. Uz najbolju predavačicu Sanidu Prevljak smo i na šaljive načine uspjeli naučiti mnogo toga, te sam uz njen trud, rad i zalaganje koji je uložila da nam na što bolji način prenese svoje stečeno znanje uspješno položio teoretski ispit.',
    },
    {
      img: '../../../assets/review3.jpg',
      name: 'Adnana Landžo',
      text: 'Najpristupačniji put do vozačkog ispita! Uz pomoć naše divne Sanide i njene posvećenosti svom radu i nesebičnog pružanja znanja sve nedoumice i prepreke za savladavanje teoretskog dijela vozačkog ispita padaju u vodu. Sve pohvale i sve preporuke od srca.',
    },
    {
      img: '../../../assets/review2.jpg',
      name: 'Larisa Halilović',
      text: 'Prije svega Sanida je dobar čovjek, a zatim i najbolji predavač. Svoje znanje na nas prenosi sa velikom profesionalnošću. Trudi se da se maksimalno posveti svakom kandidatu i da što jednostavnije pojasni. Sve preporuke za Sanidu i autoškolu "A&S" jer sigurno nećete pogriješiti u odabiru. Svojim radom i pozitivnošću uspije da sa svakim kandidatom uspostavi super komunikaciju, rad, red i disciplinu.',
    },
  ];

  faq = [
    {
      question: 'Kada i gdje se mogu upisati u autoškolu "A&S"?',
      answers: [
        'Upis novi kandidata je svaki radni dan u terminima od 8h do 18h. Prijava za novu grupu kandidata teoretske nastave se šalje svake sedmice.',
        'Šta za Vas to znači? Da svaki dan ima termin za kandidate koji su već prijavljeni na teoretsku nastavu u tekućoj sedmici i Vi na tom času ne možete prisustovati.',
        'Međutim, isti dan u drugom terminu koji Vama odgovara je vrijeme nastave za nove kandidate, kandidate koji još nemaju 18 godina (3mjeseca prije punoljetstva) , kandidate koji su odgodili izlazak na polaganje pa izlaze na iduće i rijetke kandidate ponovce.',
      ],
      isOpened: false,
    },
    {
      question: 'Koje dokumente moram imati da bi se prijavio u autoškolu?',
      answers: [
        'Za upis u autoškolu potrebno je imati ličnu kartu i ljekarsko uvjerenje. Kandidati mlađi od 18g (3mjeseca do punoljestva) mogu prisustovati teoretskoj nastavi bez lične karte. Ako imate vozačku dozvolu B kategorije i želite polagati za C1,C,CE,D1,D trebate imati važeću vozačku dozvolu za navedenu kategoriju.',
      ],
      isOpened: false,
    },
    {
      question: 'Kako se može platiti obuka?',
      answers: [
        'Nema više razloga da odgađate polaganje vozačkog ispita, niti izgovora',
        'Od proceduralnog do finansijskog dijela, učinili smo vozački ispit najpristupačnijim ikad !',
        '💸 Plaćanje na rate kompletne obuke na način na koji sami sebi odredite ratu.',
        '💰 GOTOVINSKO PLAĆANJE - POPUST !',
      ],
      isOpened: false,
    },
    {
      question: 'Koliko važi ljekarsko uvjerenje?',
      answers: [
        'Ljekarsko uvjerenje važi godinu dana od datuma vađenja ljekarskog uvjerenja.',
      ],
      isOpened: false,
    },
    {
      question:
        'Da li nudite kondicisku vožnju za vozače koji su položili a nisu dugo vozili?',
      answers: [
        'Veoma često se dogodi da položite vozački ispit i da nakon toga rijetko ili nikada ne vozite. Posljedica toga je da više niste sigurni u svoje vozačke sposobnosti i u poznavanje saobraćajnih propisa i sigurnosnih pravila. Nemojte se zabrinjavati ako niste vozili duži vremenski period, jer to je vještina koja se nikad ne zaboravlja. Možemo je usporediti sa vožnjom bicikla, naučili ste ga voziti, niste ga vozili par godina i sigurno bi ga vozili bez problema. Ipak većini vozača koji nisu dugo vozili potrebno je nekoliko sati privikavanja na tehniku vožnje i na vožnju u složenim saobraćajnim uslovima, jer automobil može biti opasno prevozno sredstvo za drugog sudionika u prometu ali i za vas. Steći ćete sigurnost u upravljanju motornim vozilom i ponoviti prometne propise i sigurnosna pravila.',
        'Naš program je individualni i potpuno se prilagođava Vašim potrebama. Vrši se analiza Vaših pogrešaka kod upravljanja vozilom te se obuka usmjerava ciljano ka poboljšanju koje dovodi do sigurnog upravljanja vozilom koje je i Vaš i naš cilj.',
      ],
      isOpened: false,
    },
  ];

  questionOpenClose(index: number) {
    // If clicking on the currently open question, close it
    if (this.openQuestion === index - 1) {
      this.faq[index - 1].isOpened = false;
      this.openQuestion = null;
    } else {
      // Close the previously open question (if any)
      if (this.openQuestion !== null) {
        this.faq[this.openQuestion].isOpened = false;
      }

      // Open the clicked question
      this.faq[index - 1].isOpened = true;
      this.openQuestion = index - 1;
    }
  }

  ngOnInit(): void {
    window.scrollTo(0, 0);
  }

  readMoreLess(phaseNumber: number) {
    // If clicking on the currently open phase, close it
    if (this.openPhase === phaseNumber) {
      this.phases[phaseNumber - 1].moreLess = false;
      this.openPhase = null;
    } else {
      // Close the previously open phase (if any)
      if (this.openPhase !== null) {
        this.phases[this.openPhase - 1].moreLess = false;
      }

      // Open the clicked phase
      this.phases[phaseNumber - 1].moreLess = true;
      this.openPhase = phaseNumber;
    }
  }

  /*   questions = document.querySelectorAll('.questions-answers'); */
}
