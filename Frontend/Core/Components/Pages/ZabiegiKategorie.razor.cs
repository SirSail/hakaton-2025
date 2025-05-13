using Microsoft.AspNetCore.Components;

namespace Core.Components.Pages
{
    public partial class ZabiegiKategorie
    {
        private string searchText = "";
        private string? SelectedTopic = null;
        private string? SelectedTab = null;
        private bool ShowDietetykaCards = true;
        private Category? SelectedCategory = null;
        private Subcategory? SelectedSubcategory = null;

        private List<string> Tabs = new() { "dokumenty", "procedury", "rehabilitacja", "dietetyka", "checklista", "terapia zajęciowa", "strategie relaksacyjne", "faq" };
        private string SelectedRehabilitationPhase = "before";



        [Parameter] public string categoryName { get; set; } = string.Empty;


        private async Task SaveFileToDownloads(string filePath)
        {
#if ANDROID

            if (string.IsNullOrEmpty(filePath))
                return;

            filePath = filePath.TrimStart('/');
            using var stream = await FileSystem.OpenAppPackageFileAsync(filePath);
            var downloadsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads)?.AbsolutePath;

            if (!string.IsNullOrEmpty(downloadsPath))
            {
                var destinationPath = Path.Combine(downloadsPath, filePath.Split("/").Last());
                using var fileStream = File.Create(destinationPath);
                await stream.CopyToAsync(fileStream);
            }
#endif
        }

        private void SelectCategory(Category cat)
        {
            SelectedCategory = cat;
            SelectedSubcategory = null;
            SelectedTopic = null;
            SelectedTab = null;
        }
        private void ToggleDietetykaCards()
        {
            ShowDietetykaCards = !ShowDietetykaCards;
        }
        private void SelectSubcategory(Subcategory sub)
        {
            SelectedSubcategory = sub;
            SelectedTopic = null;
            SelectedTab = null;
        }

        private void SelectTopic(string topic)
        {
            SelectedTopic = topic;
            SelectedTab = null;
        }

        private void SelectTab(string tab)
        {
            SelectedTab = tab;
        }
        private void SelectRehabilitationPhase(string phase)
        {
            SelectedRehabilitationPhase = phase;
        }
        private class ChecklistItem
        {
            public string Text { get; set; } = "";
            public bool IsChecked { get; set; }
        }

        private List<ChecklistItem> CheckItems_6_8 = new()
    {
        new() { Text = "krew: morfologia, OB, CRP, elektrolity (Na, K), kreatynina, parametry krzepnięcia, AlAT, AspAT" },
        new() { Text = "mocz: badanie ogólne moczu" },
        new() { Text = "EKG" },
        new() { Text = "RTG klatki piersiowej" },
        new() { Text = "RTG stawów biodrowych" },
        new() { Text = "szczepienie p/WZW-B (żółtaczka)" },
        new() { Text = "USG Doppler żył kończyn dolnych" },
        new() { Text = "kontrola dentystyczna" },
        new() { Text = "kontrola u Lekarza Rodzinnego" },
        new() { Text = "Dieta" },
        new() { Text = "Ćwiczenia" }
    };

        private List<ChecklistItem> CheckItems_1_2 = new()
    {
        new() { Text = "Aspiryna (Polopiryna, Acard, Polocard) w dawce powyżej 300mg/dziennie" },
        new() { Text = "Leki przeciwzapalne (np. Diklofenak, Majamil, Olfen, Dicloberl, Naproksen, Meloksikam, Aspikam, Piroksikam, Ibuprofen, Ibuprom, Aulin, Nimesil itp.)" },
        new() { Text = "Leki przeciwkrzepliwe np. Acenokumarol, Warfin (należy wtedy zastąpić je lekiem w iniekcji podskórnej), także Tiklopidyna czy Clopidogrel" },
        new() { Text = "Niektóre witaminy np. witamina E, K, także produkty spożywcze uzupełnione o te witaminy" },
        new() { Text = "Preparaty ziołowe (ginko-biloba, szałwia, rumianek, arnika, kasztanowiec, dziurawiec, czosnek itp.)" },
        new() { Text = "Jeżeli przyjmujesz inne leki/suplementy – zapytaj się o nie!" }
    };

        private List<ChecklistItem> CheckItems_1DayBefore = new()
    {
        new() { Text = "Zaświadczenie od stomatologa" },
        new() { Text = "Zaświadczenie od lekarza rodzinnego" },
        new() { Text = "Zaświadczenie od lekarzy prowadzących (kardiolog, chirurg naczyniowy itd.)" },
        new() { Text = "Zaświadczenie o szczepieniu przeciw WZW typu B" },
        new() { Text = "Wypełniona Karta Przygotowania Medycznego do Zabiegu Endoprotezoplastyki" },
        new() { Text = "Dokumentacja z poprzednich pobytów w szpitalu" },
        new() { Text = "Spis przyjmowanych leków z dawkowaniem" },
        new() { Text = "Wynik grupy krwi" },
        new() { Text = "Zdjęcia RTG" },
        new() { Text = "Wyniki badań USG naczyniowego" },
        new() { Text = "Inne posiadane wyniki badań" }
    };

        // Dane kategorii
        private List<Category> Categories = new()
    {
        new Category
        {
            Name = "ortopedia",
            Subcategories = new List<Subcategory>
            {
                new()
                {
                    Name = "st. biodrowy",
                    Topics = new List<string>
                    {
                        "endoproteza",
                        "zwyrodnienia",
                        "pachwina piłkarska",
                        "biodro trzaskające",
                        "artroskopia"
                    }
                },
                 new()
                {
                    Name = "st. kolanowy",
                    Topics = new List<string>
                    {
                        "endoproteza",
                        "zwyrodnienia",
                        "pachwina piłkarska",
                        "biodro trzaskające",
                        "artroskopia"
                    }
                },
                new()
                {
                    Name = "st. skokowy i stopa",
                    Topics = new List<string>
                    {
                        "operacje palców stopy",
                        "ścięgno Achillesa i pięta",
                        "ostrogi",
                        "rekonstrukcje więzadeł i chrząstki",
                        "usztywnienie stawu skokowego"
                    }
                },
                new()
                {
                    Name = "st. barkowy",
                    Topics = new List<string>
                    {
                        "dekompresja podbarkowa",
                        "rekonstrukcja więzozrostu barkowo-obojczykowego",
                        "rekonstrukcja ścięgien mięśni rotatorów",
                        "rekonstrukcja obrąbka stawowego"

                    }
                },
                new()
                {
                    Name = "st. łokciowy",
                    Topics = new List<string>
                    {
                        "łokieć tenisisty i golfisty",
                        "zespół rowka nerwu łokciowego"
                    }
                },
                new()
                {
                    Name = "nadgarstek + ręka",
                    Topics = new List<string>
                    {
                        "zespół cieśni nadgarstka",
                        "palce trzaskające",
                        "przykurcz Dupuytrena"
                    }
                },
                new()
                {
                    Name = "urazy",
                    Topics = new List<string>
                    {
                        "złamania kości",
                        "naprawa uszkodzonych tkanek miękkich",
                        "wspomaganie biologiczne leczenia urazów"
                    }
                }
            }
        },
        new Category
        {
            Name = "kardiologia",
            Subcategories = new List<Subcategory>
            {
                new()
                {
                    Name = "st. serca",
                    Topics = new List<string>
                    {
                        "zawał",
                        "niewydolność serca",
                        "kardiowersja"
                    }
                }
            }
        }
    };
        private List<CardItem> RehabilitacjaCardsBefore = new()
{
     new CardItem
    {
        Title = "Klęk podparty z nogą w przód",
        Description = (MarkupString)@"
            <ol>
                <li>Wykonaj klęk podparty na macie</li>
                <li>Jedną nogę przenieś w przód między dłonie, a drugą wyprostuj</li>
                <li>Staraj się wyprostować plecy, dłonie mogą pozostać oparte na podłodze lub oprzyj je na kolanie</li>
                <li>Z każdym wydechem próbuj zejść biodrem coraz niżej w dół</li>
                <li>Utrzymuj pozycję od 30-60s, a następnie zmień stronę </li>
                <li>Wykonuj ćwiczenie codziennie po 2 razy na każdą stronę</li>
            </ol>",
        ImagePath = "/Images/rehabilitacja/wyprost.jpg"
    },
    new CardItem
    {
        Title = "Ptak-pies",
        Description = (MarkupString)@"
            <ol>
                <li>Wykonaj klęk podparty na macie</li>
                <li>Zepnij mocno brzuch</li>
                <li>Głowa i kręgosłup powinny być w jednej linii</li>
                <li>Spróbuj podnieść jednocześnie prawą nogę i lewą rękę w górę, a następnie zmień stronę. Jeśli okaże się to za trudne podnoś osobno nogę i rękę</li>
                <li>Zwróć uwagę na odcinek lędźwiowy kręgosłupa. Nie może on wyginać się podczas ćwiczenia. Jeżeli tak się dzieje, to warto wzmocnić dodatkowo mięśnie brzucha</li>
                <li>Wykonuj ćwiczenie w 3 seriach po 6-8 powtórzeń na każdą stronę</li>
            </ol>",
        ImagePath = "/Images/rehabilitacja/zuraw.jpg"
    },
    new CardItem
    {
        Title = "Unoszenie nogi bokiem w podporze bocznym",
        Description = (MarkupString)@"
            <ol>
                <liPołóż się na boku </li>
                <li>Oprzyj się na przedramieniu po tej samej stronie, na której leżysz.</li>
                <li>Dla równowagi możesz podeprzeć się na dłoni drugiej ręki</li>
                <li>Zadbaj o to, by Twoje ciało było w jednej linii</li>
                <li>Zepnij mocno brzuchb</li>
                <li>Unieś nogę do góry, następnie przytrzymaj 3-5s i opuść w dół</li>
                <li>Przy kolejnych powtórzeniach staraj się nie odkładać bezwładnie nogi na nogę, a dopiero opuścić ją po skończonej serii</li>
                <li>Wykonuj ćwiczenie w 3 seriach po 6-8 powtórzeń na każdą stronę</li>
            </ol>",
        ImagePath = "/Images/rehabilitacja/miednica.jpg"
    },
    new CardItem
    {
        Title = "Most biodrowy",
        Description = (MarkupString)@"
            <ol>
                <li>Połóż się na plecach i ugnij nogi w kolanach</li>
                <li>Zepnij mocno brzuch</li>
                <li>Wznieś biodra w górę. Nie wyginaj kręgosłupa, ciało powinno być w jednej linii</li>
                <li>Zepnij pośladki i utrzymaj pozycję przez 5–10 sekund</li>
                <li>Odłóż powoli ciało na matę. Wykonuj to w kontrolowany sposób</li>
                <li>Wykonuj ćwiczenie w 3 seriach po 6–8 powtórzeń</li>
            </ol>",
        ImagePath = "/Images/rehabilitacja/pupa.jpg"
    },
    new CardItem
    {
        Title = "Wznosy nóg w leżeniu",
        Description = (MarkupString)@"
            <ol>
                <li>Połóż się na plecach, nogi pozostaw wyprostowane</li>
                <li>Skieruj palce stóp na siebie</li>
                <li>Wykonaj 6–8 razy powolny wznos i opust prawej nogi</li>
                <li>Następnie przenieś prawą nogę do góry i chwyć dłońmi pod udem</li>
                <li>Utrzymaj pozycję przez 30 sekund</li>
                <li>Wykonaj to samo na lewą stronę</li>
                <li>Wykonuj ćwiczenie w 3 seriach</li>
            </ol>",
        ImagePath = "/Images/rehabilitacja/swieca.jpg"
    },
    new CardItem
    {
        Title = "Przyciąganie kolana do klatki piersiowej",
        Description = (MarkupString)@"
            <ol>
                <li>Połóż się na plecach, nogi pozostaw wyprostowane</li>
                <li>Zegnij prawą nogę w kolanie i przyciągnij ją do klatki piersiowej</li>
                <li>Kręgosłup powinien w całości leżeć na macie</li>
                <li>Utrzymaj pozycję przez 30 sekund, a następnie zmień nogę</li>
                <li>Wykonuj ćwiczenie w 4 seriach</li>
            </ol>",
        ImagePath = "/Images/rehabilitacja/zgiecie.jpg"
    }

};

        private List<CardItem> RehabilitacjaCardsAfter = new()
{
    new CardItem
    {
        Title = "Instrukcja 1",
        Description = (MarkupString)@"
            <p>❌ Nie zakładać nogi na nogę w pozycji stojącej, siedzącej i leżącej</p>
            <p>❌ W pozycji siedzącej nie podnosić kolan powyżej bioder</p>
            <p>❌ Nie siedzieć dłużej niż 1 godzinę bez wstawania</p>
            <p> ✅ Należy rozstawić wygodnie kolana  </p>",
        ImagePath = "/Images/rehabilitacja/siedziec.jpg"
    },
    new CardItem
    {
        Title = "Instrukcja 2",
        Description = (MarkupString)@"
                <p>
                    ❌ Nie należy wykonywać zbyt głębokich skłonów podczas ani nie opuszczać rąk poniżej kolan
                </p>
                <p> ✅ W celu sięgnięcia po coś warto korzystać z chwytaka lub pomocy drugiej osoby </p>",
        ImagePath = "/Images/rehabilitacja/sklon.jpg"
    },
    new CardItem
    {
        Title = "Instrukcja 3",
        Description = (MarkupString)@"
            <p> ❌ Podczas chodu nie należy kierować stóp ku sobie z palcami zwróconymi do środka</p>
            <p> ❌ Przy zmianie kierunku chodu należy unikać krzyżowania nóg </p>
            <p> ✅ Poprawny chód warto ćwiczyć przed operacją wraz z fizjoterapeutą  </p>",
        ImagePath = "/Images/rehabilitacja/dziad.jpg"
    },
    new CardItem
    {
        Title = "Instrukcja 4",
        Description = (MarkupString)@"
            <p>
                ❌ Podczas wstawania nie należy gwałtownie przechylać się do przodu    
            </p>
            <p> ✅ Powinno się najpierw delikatnie pochylić tułów w przód i dopiero potem wstać </p>",
        ImagePath = "/Images/rehabilitacja/wstawac.jpg"
    },
    new CardItem
    {
        Title = "Instrukcja 5",
         Description = (MarkupString)@"
                <p> ❌ Nie należy krzyżować ani skręcać nóg podczas leżenia</p>
                <p> ❌ Nie można spać bez poduszki między nogami</p>
                <p> ❌ Nie należy leżeć na stronie operowanej </p>
                <p> ✅ Kładąc się na stronie nieoperowanej, umieść 1-2 poduszki między lekko zgiętymi nogami</p>
                <p> ✅ Należy przez 15-30 min dziennie leżeć płasko na łóżku  </p>",
         ImagePath = "/Images/rehabilitacja/lezec.jpg"
    },
    new CardItem
    {
        Title = "Instrukcja 6",
        Description = (MarkupString)@"
            <p> ❌ Nie należy siadać na niskim sedesie, kanapie oraz krześle </p>
            <p> ❌ Nie należy siadać na miękkich obiciach mebli</p>
            <p> ✅ Należy stosować nakładkę podwyższającą na toaletę  </p>
            <p> ✅ Podczas siedzenia należy utrzymywać nogi w rozstawieniu  </p>",
        ImagePath = "/Images/rehabilitacja/srac.jpg"
    },
    new CardItem
    {
        Title = "Instrukcja 7",
        Description = (MarkupString)@"
            <p> ❌ Należy unikać schylania się o więcej niż 80 stopni</p>
            <p> ❌ Podczas siadania i wstawania nie można dopuszczać, by barki wychylały się dalej niż biodra </p>
            <p> ✅ Stolik nocny należy umieścić po stronie nieoperowanej  </p>",
        ImagePath = "/Images/rehabilitacja/przykrywac.jpg"
    },
    new CardItem
    {
        Title = "Instrukcja 8",
        Description = (MarkupString)@"
            <p> ✅ Podczas wchodzenia po schodach najpierw stawiamy nogę zdrową, następnie operowaną, a na końcu obie kule </p>
            <p> ✅ Podczas schodzenia po schodach najpierw na stopniu niżej stawiamy obie kule, następnie nogę operowaną, a na końcu nogę zdrową </p>
            <p> ✅ Praktykuj schodzenie i wchodzenie ze wsparciem, aby uniknąć kontuzji  </p>",
        ImagePath = "/Images/rehabilitacja/kule.jpg"
    },

};


        private List<CardItem> ProceduryCards = new()
{
    new CardItem
    {
            Title = "Wszystko o zabiegu – endoprotezoplastyka stawu biodrowego",
            Description = (MarkupString)@"
        <p><strong>🦴 Co to jest endoprotezoplastyka?</strong><br/>
        To operacja polegająca na zastąpieniu chorego stawu biodrowego jego sztucznym odpowiednikiem – endoprotezą. Pomaga osobom z bólem, sztywnością i ograniczonym ruchem. Dla wielu to <em>„druga młodość” biodra</em> – znów mogą chodzić, spać i żyć bez bólu.</p>

        <ul>
            <li>✅ Najczęściej wykonywana przy złamaniach szyjki kości udowej i zaawansowanej chorobie zwyrodnieniowej</li>
            <li>✅ Rutynowy zabieg – codziennie przeprowadza się ich setki</li>
            <li>✅ Nowoczesne, trwałe materiały – biokompatybilne i bezpieczne</li>
            <li>✅ Dobre przygotowanie = szybszy powrót do sprawności</li>
        </ul>

        <p>Proteza składa się z trzech części: <strong>panewki, głowy i trzpienia</strong>.</p>

        <p><strong>💉 Znieczulenie</strong><br/>
        Najczęściej stosuje się znieczulenie podpajęczynówkowe – pacjent jest przytomny, ale nie odczuwa bólu od pasa w dół. W razie potrzeby można podać leki uspokajające lub nasenne. Warto omówić to wcześniej z anestezjologiem.</p>

        <p><strong>🔧 Przebieg operacji</strong><br/>
        Cięcie wykonywane jest z przodu, boku lub tyłu – w zależności od potrzeb pacjenta. Operacja trwa zwykle 1–3 godziny. Przed zabiegiem konieczne jest przygotowanie: mycie ciała, golenie operowanego miejsca, znieczulenie.</p>

        <p>Endoproteza może być osadzona:</p>
        <ul>
            <li>🔹 z użyciem cementu kostnego (działa jak klej)</li>
            <li>🔹 bez cementu (kość naturalnie zrasta się z protezą)</li>
        </ul>

        <p><strong>📝 Co trzeba zrobić przed operacją?</strong></p>
        <ul>
            <li>🩸 Badania krwi i moczu</li>
            <li>🩻 RTG klatki piersiowej</li>
            <li>📃 Zaświadczenia od lekarza rodzinnego i specjalistów</li>
            <li>🦷 Wizyta u dentysty – wykluczenie infekcji</li>
            <li>👩‍⚕️ Inne konsultacje w razie potrzeby (laryngolog, ginekolog, urolog)</li>
        </ul>

        <p><strong>💊 Co z lekami?</strong><br/>
        Możliwa tymczasowa zmiana leków (np. na nadciśnienie, cukrzycę, przeciwzakrzepowe) – decyzję podejmuje lekarz prowadzący wraz z anestezjologiem.</p>

        <p><strong>🏥 Co Cię czeka po operacji?</strong></p>
        <ol>
            <li><strong>Dzień po zabiegu:</strong><br/> 🔹 Pomoc fizjoterapeuty w wstawaniu<br/> 🔹 Nauka chodzenia z balkonikiem lub kulami</li>
            <li><strong>Pobyt w szpitalu (3–7 dni):</strong><br/> 🔹 Ćwiczenia<br/> 🔹 Nauka bezpiecznego poruszania się</li>
            <li><strong>Po wypisie:</strong><br/> 🔹 Kontynuacja rehabilitacji domowej lub ambulatoryjnej</li>
        </ol>

        <p><strong>🩹 Co robić po wyjściu ze szpitala?</strong></p>
        <ul>
            <li><strong>Opatrunek:</strong> zmieniaj co 2–3 dni w czystych warunkach</li>
            <li><strong>Uwaga na ranę:</strong> jeśli sączy się wydzielina – natychmiast skontaktuj się z lekarzem</li>
            <li><strong>Szwy:</strong> zdejmowane zwykle między 10. a 14. dniem po zabiegu</li>
        </ul>

        <p><em>Jesteś dobrze przygotowany? To połowa sukcesu! Pamiętaj – zespół medyczny jest po to, by Ci pomóc.</em></p>
    ",
            ImagePath = "/Images/procedury/wszystko_o_zabiegu.jpg"
    }
};


        private List<CardItem> DietetykaCards = new()
{
    new CardItem
    {
        Title = "Przed zabiegiem",
        Description = (MarkupString)@"
            <p>Twoje ciało potrzebuje siły, żeby dobrze znieść operację i szybko się zregenerować. Dobra dieta i odpowiednie wsparcie od wewnątrz mogą naprawdę przyspieszyć powrót do zdrowia. Zadbaj o to, by codzienne posiłki były pełnowartościowe. Jedz regularnie i nie pomijaj posiłków. W Twoim menu powinno się znaleźć:</p>
            <ul>
                <li><strong>Białko</strong> – mięso, ryby, jajka, nabiał, rośliny strączkowe</li>
                <li><strong>Warzywa i owoce - dostarczają witamin i błonnika</strong> – szpinak, jarmuż, pietruszka, rukola, buraczki, ciecierzyca, fasola, cukinia, dynia, pomidory, marchew, jabłka</li>
                <li><strong>Zdrowe tłuszcze     - są istotne dla funkcjonowania układu
                    sercowo-naczyniowego, mózgu i układu odpornościowego,
                     są budulcem komórek, dostarczają energii,
                    wspierają wchłanianie witamin A, D, E, K</strong> – oliwa z oliwek, orzechy, awokado</li>
                <li><strong>Produkty pełnoziarniste - dla energii i dobrego trawienia</strong> – kasza gryczana, kasza jaglana, płatki owsiane, pieczywo pełnoziarniste, makaron razowy</li>
            </ul>",
        ImagePath = "/Images/dietetyka/awokado.jpg"
    },
    new CardItem
    {
        Title = "Unikaj",
        Description = (MarkupString)@"
            <p>Unikaj:</p>
            <ul>
                <li>❌przetworzonej żywności</li>
                <li>❌dużych ilości cukru</li>
                <li>❌alkoholu</li>
                <li>❌tłuszczów trans- można je znaleźć w takich produktach jak margaryna, majonez, dania typu fast food, tłuszcze zwierzęce, chipsy,zupy w proszku, wyroby cukiernicze, krakersy, pączki, pizza</li>
            </ul>",
        ImagePath = "/Images/dietetyka/oleje.jpg"
    },
    new CardItem
    {
        Title = "💊 Suplementacja przed zabiegiem",
        Description = (MarkupString)@"
            <p>Przed zabiegiem będziesz miał robione szczegółowe badania krwi. Jeśli masz niedobory (np. żelaza, witaminy D, B12, kwasu foliowego), lekarz może zalecić odpowiednie suplementy. Warto zapytać też o preparaty wspierające odporność. Pamiętaj – nie bierz niczego na własną rękę, zawsze skonsultuj to z lekarzem.</p>
            <p>Warto również zadbać o:
            <ul>
                <li>Witaminę D3 (jesień/zima)</li>
                <li>Probiotyki</li>
                <li>Kolagen, cynk, witaminę C</li>
            </ul></p>",
        ImagePath = "/Images/dietetyka/lyzeczki.jpg"
    },
    new CardItem
    {
        Title = "🥣 Po operacji – regeneracja",
        Description = (MarkupString)@"
            <p>Po operacji zapotrzebowanie na składniki odżywcze jeszcze bardziej rośnie. Warto kontynuować zdrową dietę i dbać o nawodnienie (woda, lekkie zupy, herbatki ziołowe).
            Czasami po operacji potrzebne będą dodatkowe leki osłonowe na żołądek, probiotyki lub preparaty żelaza – zależnie od przebiegu leczenia.</p>
            <p>Co warto zabrać do szpitala: :
            <ul>
                <li>Woda, lekkie zupy, herbaty ziołowe</li>
                <li>Musy owocowe, suszone owoce (figi, morele)</li>
                <li>Orzechy (np. nerkowce), pasty warzywne</li>
                <li>Biszkopty, wafle ryżowe, batony owocowe bez cukru</li>
            </ul></p>",
        ImagePath = "/Images/dietetyka/orzech.jpg"
    }
};
        private List<Dokument> Dokumenty = new()
{
    new Dokument
    {
        Title = "Zgoda na zabieg endoprotezy",
        Description = "Dokument do podpisu przed operacją. Prosimy o dokładne zapoznanie się.",
        FilePath = "/docs/zgoda_na_zabieg_oraz_ocena_anestezjologa.pdf",
        FileType = "PDF"
    },
    new Dokument
    {
        Title = "Instrukcja przedoperacyjna stara",
        Description = "Zalecenia jak przygotować się do zabiegu.",
        FilePath = "/docs/zalecenia_stare.docx",
        FileType = "PDF"
    },
    new Dokument
    {
        Title = "Instrukcja przedoperacyjna nowa",
        Description = "Zalecenia jak przygotować się do zabiegu.",
        FilePath = "/docs/zalecenia_nowe.docx",
        FileType = "PDF"
    },
    new Dokument
    {
        Title = "Prospekt 1 Endoproteza",
        Description = "Dokument do zapoznania się przed operacją. Prosimy o dokładne przeczytanie.",
        FilePath = "/docs/Prospekt1_Endoproteza_stawu_biodrowego.pdf",
        FileType = "PDF"
    },
    new Dokument
    {
        Title = "Prospekt 2 Endoproteza",
        Description = "Dokument do zapoznania się przed operacją. Prosimy o dokładne przeczytanie.",
        FilePath = "/docs/Prospekt_Nr_2_Endoproteza_st_kolanowego.pdf",
        FileType = "PDF"
    },
        new Dokument
    {
        Title = "Zalecenia profesora Sibińskiego",
        Description = "Dokument zawierający zalecenia lekarskie przygotowane przez prof. Sibinskiego",
        FilePath = "/docs/zalecenia_prof_Sibinskiego.pdf",
        FileType = "PDF"
    }
};
        private List<HybridItem> TerapiaZajeciowaCards = new()
{
    new HybridItem
    {
        Title = "Mandale do kolorowania",
        Description = (MarkupString)@"
            <p>Pobierz paczkę mandali w formacie ZIP do własnego wydruku i kolorowania.</p>",
        FilePath = "/Images/terapia/mandale.zip",
        FileType = "ZIP",
        ImagePath = "/Images/terapia/zip.jpg"
    },
    new HybridItem
    {
        Title = "Mandale – twórcza równowaga",
        Description = (MarkupString)@"
            <p>Mandale to symetryczne wzory tworzone intuicyjnie. Nie musisz mieć zdolności artystycznych – liczy się proces, nie efekt!</p>
            <p>Kolorowanie mandali pomaga się wyciszyć, zrelaksować i odzyskać równowagę emocjonalną. To forma medytacji w działaniu.</p>",
        ImagePath = "/Images/terapia/mandala1.jpg"
    },
    new HybridItem
    {
        Title = "Jak zacząć?",
        Description = (MarkupString)@"
            <p>Wybierz mandalę z naszego zbioru lub narysuj własną. Nie przejmuj się, jeśli rysunek nie będzie idealny – chodzi o twórczy proces.</p>
            <p>Użyj kredek, flamastrów lub farb. Poczuj barwy, oddychaj spokojnie. To czas tylko dla Ciebie.</p>",
        ImagePath = "/Images/terapia/mandala2.jpg"
    },
    new HybridItem
    {
        Title = "Dlaczego to działa?",
        Description = (MarkupString)@"
            <p>Mandala działa jak lustro – odzwierciedla Twój nastrój, a jej symetria wprowadza harmonię do myśli i emocji.</p>
            <p>To narzędzie wspierające koncentrację, redukcję stresu i autorefleksję – idealne dla osób w trakcie rekonwalescencji.</p>",
        ImagePath = "/Images/terapia/mandala3.jpg"
    },
    new HybridItem
    {
        Title = "Twoja codzienna mandala",
        Description = (MarkupString)@"
            <p>Zarezerwuj codziennie 10–15 minut na kolorowanie. Możesz wybrać różne wzory w zależności od nastroju lub pory dnia.</p>
            <p>Obserwuj, jak zmienia się Twój nastrój. Prowadź dziennik kolorów lub zapisz, co czułeś podczas rysowania.</p>",
        ImagePath = "/Images/terapia/mandala4.jpg"
    },
    new HybridItem
    {
        Title = "Znaczenie kolorów w mandali",
        Description = (MarkupString)@"
            <p><strong>Interpretacja niektórych kolorów:</strong></p>
            <ul>
                <li><strong>Biały:</strong> Symbolizuje czystość, niewinność, doskonałość, szczerość oraz, w niektórych przypadkach, początek i nowe możliwości.</li>
                <li><strong>Czarny:</strong> Może oznaczać tajemniczość, smutek, ale także siłę, honor, odradzanie czy powrót na właściwą ścieżkę.</li>
                <li><strong>Czerwony:</strong> Reprezentuje energię, pasję, miłość, ale również gniew i intensywność uczuć.</li>
                <li><strong>Pomarańczowy:</strong> Symbolizuje radość, optymizm, energię, ale także odwagę i ambicję.</li>
                <li><strong>Żółty:</strong> Oznacza radość, światło, intelekt, mądrość oraz pogodę ducha.</li>
                <li><strong>Zielony:</strong> Symbolizuje nadzieję, życie, równowagę, uzdrowienie i nową perspektywę.</li>
                <li><strong>Niebieski:</strong> Oznacza spokój, rozluźnienie, opanowanie, ale także intuicję i duchowość.</li>
                <li><strong>Fioletowy:</strong> Może symbolizować uduchowienie, inspirację, uczciwość, a także duchową głębię.</li>
                <li><strong>Różowy:</strong> Reprezentuje kobiecość, delikatność, romantyzm i miłość.</li>
            </ul>",
        ImagePath = "/Images/terapia/mandala5.jpg"
    }

};
        private List<CardItem> StrategieRelaksacyjneCards = new()
{

    new CardItem
    {
        Title = "Wszystko zaczyna się od oddechu",
        Description = (MarkupString)@"
            <p>Wszystko zaczyna się od oddechu
            Jak oddychanie trójkątne i box breathing mogą poprawić Twoje życie
            W codziennym biegu, stresie, pośpiechu i natłoku myśli często zapominamy o czymś, co mamy zawsze przy sobie – oddechu. Oddychasz przez cały dzień, ale czy kiedykolwiek świadomie oddychałeś? Prawda jest taka, że nasz oddech to kotwica, która może nas uspokoić, poprawić koncentrację, pomóc w zasypianiu, a nawet zwiększyć wytrzymałość fizyczną.
            To nie magia – to biologia. Świadome oddychanie aktywuje przywspółczulny układ nerwowy, który odpowiada za stan relaksu, trawienia i regeneracji. W ten sposób „przełączasz się” z trybu „walcz lub uciekaj” (stres) na tryb „odpocznij i wyzdrowiej”.
            Chciałbym dać Ci dziś dwie bardzo proste, ale potężne techniki oddechowe: Box Breathing (oddychanie kwadratowe lub inaczej pudełkowe) i Triangle Breathing (oddychanie trójkątne). Obie możesz wykorzystać, by się uspokoić, zredukować stres, ale też jako trening oddechowy – np. poprawiający Twoją wytrzymałość oddechową..</p>
            <p><strong>Techniki takie jak Box Breathing i Triangle Breathing</strong> pomagają:</p>
            <ul>
                <li>Uspokoić ciało i myśli</li>
                <li>Poprawić sen i koncentrację</li>
                <li>Zwiększyć wytrzymałość i odporność psychiczną</li>
            </ul>
            <p>Najlepsze jest to, że możesz wykonywać je w zasadzie w każdej pozycji i każdym miejscu, więc idealnie sprawdzą się jako Twoje asy w rękawie, czy to w rehabilitacji i poprawie wydolności, czy też jako zwalczacz stresu.</p>",
        ImagePath = "/Images/relaks/oddech.jpg"
    },
    new CardItem
    {
        Title = "Oddychanie Trójkątne (Triangle Breathing)",
        Description = (MarkupString)@"
            <p>Proste i skuteczne ćwiczenie, które pomoże Ci się wyciszyć, skupić i poczuć się lepiej – niezależnie od sytuacji.
            Na czym polega?
            Wyobraź sobie trójkąt. Każdy jego bok to jedna faza oddechu: wdech, wstrzymanie i wydech – każda trwająca tyle samo czasu</p>
            <ol>
                <li>Weź głęboki wdech NOSEM przez 3 sekund – poczuj, jak rozszerza się Twoja klatka piersiowa i brzuch.</li>
                <li>Wstrzymaj oddech na 3 sekund – zachowaj spokój, nie spinaj się.</li>
                <li>Zrób powolny wydech NOSEM przez 3 sekund – nie wypychaj powietrza siłą, wypuszczaj je spokojnie</li>
            </ol>
            <p>Powtarzaj przez 2 minuty. Cały czas licz!</p>",
        ImagePath = "/Images/relaks/trojkat.jpg"
    },
    new CardItem
    {
        Title = "Wizualizacja i efekty Triangle Breathing",
        Description = (MarkupString)@"
            <p>Wyobraź sobie 🔺 – każda krawędź to 5 sekund. ➡️ Wdech → Wstrzymanie → Wydech Rysuj go w wyobraźni lub palcem w powietrzu.
            Możesz sobie wyobrażać, że rysujesz palcem trójkąt w powietrzu lub zamknąć oczy i zobaczyć go w wyobraźni. To pomoże Ci jeszcze lepiej się skupić.
            Gdy pojawiają się myśli:
            To normalne, że w trakcie ćwiczenia pojawią się myśli. Nie walcz z nimi. Zauważ je, powiedz sobie:
            „Widzę cię, myśli. Wracam do mojego oddechu.”
            I znów skup się na liczeniu 5…5…5.</p>
            <p><strong>Co zyskasz?</strong></p>
            <ul>
                <li>Uspokojenie nerwów</li>
                <li>Poprawę koncentracji i jakości snu</li>
                <li>Zmniejszy napięcie w mięśniach</li>
                <li>Może być też świetnym treningiem oddechu – jeśli ćwiczysz, pływasz lub grasz w sport</li>
            </ul>",
        ImagePath = "/Images/relaks/wizualizacja_trojkat.jpg"
    },
    new CardItem
    {
        Title = "Box Breathing (Oddychanie Pudełkowe)",
        Description = (MarkupString)@"
            <p>To technika stosowana przez sportowców, medytujących, a nawet żołnierzy Navy SEAL. Dlaczego? Bo działa. Pomaga w szybkim odzyskaniu spokoju i kontroli nad emocjami – w każdej sytuacji.</p>
            <p><strong>Instrukcja:</strong></p>
            <ol>
                <li>Wdech – 3 sekundy</li>
                <li>Wstrzymanie – 3 sekundy</li>
                <li>Wydech – 3 sekundy</li>
                <li>Wstrzymanie po wydechu – 3 sekundy</li>
                <li>Powtórz cykl 5–10 razy.</li>
            </ol>
            <p> Nie zniechęcaj się jak nie uda Ci się zrobić 5. Stopniowo trenuj a uda Ci się!
             Wizualizacja:
            Wdech → Zatrzymanie → Wydech → Zatrzymanie
            (pomyśl o rysowaniu boków kwadratu, każde 4 sekundy)</p>",
        ImagePath = "/Images/relaks/box.jpg"
    },
    new CardItem
    {
        Title = "Korzyści z Box Breathing",
        Description = (MarkupString)@"
            <p><strong>Dlaczego warto?</strong> Box Breathing to szybkie i skuteczne narzędzie do odzyskania spokoju. Idealne w stresujących momentach – wystarczy kilka oddechów, by odzyskać równowagę.</p>
            <p><strong>✅ Co zyskujesz?</strong></p>
            <ul>
                <li>⏱️ Redukcja stresu i napięcia niemal natychmiast</li>
                <li>🔁 Lepsza kontrola nad oddechem i ciałem</li>
                <li>🛡️ Wzrost odporności psychicznej</li>
                <li>🎯 Większe skupienie i klarowność myślenia</li>
                <li>📌 Sprawdza się w pracy, przed egzaminem, wystąpieniem lub treningiem</li>
            </ul>
            <p>To jedno z najprostszych i najpotężniejszych ćwiczeń mentalnych, jakie możesz mieć zawsze przy sobie.</p>",
        ImagePath = "/Images/relaks/box_korzysci.jpg"
    },

    new CardItem
    {
        Title = "Dlaczego warto oddychać świadomie?",
        Description = (MarkupString)@"
            <p>Twój oddech to Twój dostęp do spokoju – w każdej chwili.
            Nie potrzebujesz aplikacji, sprzętu ani specjalnego miejsca. Wystarczy Ty, Twoje ciało i chwila czasu. Praktykuj Triangle Breathing i Box Breathing regularnie, a zauważysz, jak Twoje ciało się rozluźnia, umysł się wycisza, a życie staje się trochę lżejsze.
            To drobna rzecz, ale może przynieść wielką zmianę.</p>
            <p>🔁 <strong>Ćwicz regularnie:</strong></p>
            <ul>
                <li>Przed rozpoczęciem dnia (np. przed śniadaniem)</li>
                <li>💼 W ciągu dnia w stresujących momentach (np. w pracy, na uczelni)</li>
                <li>🌙 Przed snem – jako sposób na wyciszenie</li>
            </ul>
           " ,
        ImagePath = "/Images/relaks/podsumowanie.jpg"
    }
};
        private List<CardItem> FaqCards = new()
{
    new CardItem
    {
        Title = "FAQ - Najczęstsze pytania przed operacją endoprotezy",
        Description = (MarkupString)@"
            <ul class='list-unstyled'>
                <li>
                    <strong>Jak długo trwa operacja?</strong><br/>
                    Ok. 1,5 - 3h.
                </li>
                <li class='mt-3'>
                    <strong>Jaki rodzaj endoprotezy zostanie w moim zabiegu użyty?</strong><br/>
                    Od rodzaju użytej endoprotezy decyduje lekarz ortopeda. Dobierana jest ona indywidualnie w zależności od stanu kości, stanu zdrowia pacjenta, wieku oraz poziomu aktywności.
                </li>
                <li class='mt-3'>
                    <strong>Czy będę musiał wymieniać moją endoprotezę?</strong><br/>
                    Jest to kwestia indywidualna. Endoprotezę wymienia się co ok 15 - 30 lat.
                </li>
                <li class='mt-3'>
                    <strong>Czy muszę się odchudzać przed operacją?</strong><br/>
                    Jeśli twoja waga przekracza normę, warto przed operacją udać się do dietetyka w celu zredukowania masy ciała. Zmniejszy to ryzyko powikłań pooperacyjnych.
                </li>
                <li class='mt-3'>
                    <strong>Czy mogę wykrwawić się podczas operacji?</strong><br/>
                    Decyzję o dopuszczeniu do zabiegu podejmuje lekarz prowadzący wraz z zespołem medycznym.
                    Przed planowaną operacją wykonuje się szereg badań i konsultacji w celu zminimalizowania ryzyka.
                    Każda operacja wiąże się z możliwością powikłań, dlatego wszelkie wątpliwości należy skonsultować z lekarzem.
                    Pacjent przed zabiegiem otrzymuje dokumentację, w której wyraża zgodę na zabieg.
                </li>
                <li class='mt-3'>
                    <strong>Czy po operacji mogę dostać zakrzepicy?</strong><br/>
                    W celu zminimalizowania zakrzepicy pacjenci po zabiegach operacyjnych otrzymują indywidualnie dobrane leki oraz wykonują ćwiczenia przeciwzakrzepowe.
                    Czasami dodatkowo zaleca się noszenie pończoch uciskowych.
                </li>
                <li class='mt-3'>
                    <strong>Czy w wyniku operacji mogę stracić czucie w kończynach?</strong><br/>
                    Jednym z powikłań operacyjnych może być uszkodzenie nerwu skutkujące zaburzeniami czucia w jego obszarze.
                    Jest to stosunkowo rzadkie powikłanie, jednakże może się zdarzyć.
                    Pacjent zostaje o tym poinformowany przed operacją i wypełnia zgodę świadczącą o tym, że zapoznał się z możliwymi powikłaniami.
                </li>
                <li class='mt-3'>
                    <strong>Jak długo będę w szpitalu po operacji?</strong><br/>
                    Pobyt w szpitalu po operacji trwa zazwyczaj 3-7 dni, jednak wszystko zależy od stanu pacjenta.
                    W przypadkach, kiedy wystąpiły ciężkie powikłania pobyt może się przedłużyć.
                </li>
                <li class='mt-3'>
                    <strong>Kiedy będę mógł chodzić?</strong><br/>
                    Pionizacja pacjentów najczęściej odbywa się w pierwszej dobie po operacji, a chodzenie w kolejnych dniach.
                    Jest to jednak kwestia indywidualna i zależy od przebiegu operacji, powikłań rodzaju endoprotezy i ogólnego stanu zdrowia pacjenta.
                </li>
                <li class='mt-3'>
                    <strong>Czy będę potrzebował pomocy w domu po operacji?</strong><br/>
                    Pomoc po operacji jest bardzo przydatna. Warto zaopatrzyć się wcześniej w chwytak i podwyższające nakładki na sedes oraz krzesła.
                </li>
                <li class='mt-3'>
                    <strong>Kiedy będę mógł wrócić do pracy?</strong><br/>
                    Okres rekonwalescencji zależy od przebiegu operacji, typu użytej endoprotezy, powikłań, ogólnego stanu zdrowia oraz rehabilitacji.
                    Zazwyczaj trwa on od 6 tygodni do 6 miesięcy.
                </li>
                <li class='mt-3'>
                    <strong>Czy będę mógł uprawiać sport po zabiegu?</strong><br/>
                    Tak, jednak warto to skonsultować z lekarzem oraz fizjoterapeutą.
                    Należy unikać skoków oraz sportów kontaktowych, ponieważ grozi to zwichnięciem endoprotezy.
                </li>
            </ul>"
    }
};


        private class Dokument
        {
            public string Title { get; set; } = "";
            public string Description { get; set; } = "";
            public string FilePath { get; set; } = "";
            public string FileType { get; set; } = "";
        }


        private class Category
        {
            public string Name { get; set; } = "";
            public List<Subcategory> Subcategories { get; set; } = new();
        }

        private class Subcategory
        {
            public string Name { get; set; } = "";
            public List<string> Topics { get; set; } = new();
        }

        private class CardItem
        {
            public string Title { get; set; } = "";
            public MarkupString Description { get; set; }
            public string ImagePath { get; set; } = "";
        }
        public class HybridItem
        {
            public string Title { get; set; } = "";
            public MarkupString Description { get; set; }
            public string? ImagePath { get; set; } = "";
            public string? FilePath { get; set; }
            public string? FileType { get; set; }
        }

    }
}

