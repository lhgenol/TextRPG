namespace TextRPG;

class Character
{
    // 속성 지정 (프로퍼티)
    public int Level { get; set; }
    public string Name { get; set; }
    public string Job { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Health { get; set; }
    public int Gold { get; set; }

    // 디폴트 생성자
    public Character()
    {
        Level = 1;
        Name = "UnKnown";
        Job = "UnKnown";
        Attack = 0;
        Defense = 0;
        Health = 0;
        Gold = 0;
    }
    
    // 생성자 (초기값 설정)
    public Character(string name, string job)
    {
        Level = 1;
        Name = name;
        Job = job;
        Attack = 10;
        Defense = 5;
        Health = 100;
        Gold = 1500;
    }

    // 상태 출력 메서드
    public void ShowStatus()
    {
        Console.WriteLine($"Lv : 0{Level}");
        Console.WriteLine($"{Name} ({Job})");
        Console.WriteLine($"공격력 : {Attack}");
        Console.WriteLine($"방어력 : {Defense}");
        Console.WriteLine($"체력 : {Health}");
        Console.WriteLine($"Gold : {Gold} G");
    }
}

class Inventory
{
    public List<Item> Items { get; set; }

    public Inventory()
    {
        Items = new List<Item>();  // 처음엔 비어 있음
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }
    
    public void ShowInventory()
    {
        Console.WriteLine("\n인벤토리\n보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine("\n[아이템 목록]");

        if (Items.Count == 0)
        {
            Console.WriteLine("인벤토리가 비어 있습니다.");
            return;
        }

        foreach (Item item in Items)
        {
            string equippedMarker;
            if (item.IsEquipped)
            {
                equippedMarker = "[E]";
            }
            else
            {
                equippedMarker = " ";
            }
            
            string stats;
            if (item.Attack > 0)
            {
                stats = $"공격력 +{item.Attack}";
            }
            else
            {
                stats = $"방어력 +{item.Defense}";
            }
            
            Console.WriteLine($"{equippedMarker} {item.Name} | {stats} | {item.Description}");
        }
        
        Console.WriteLine("\n1. 장착 관리\n2. 나가기");
    }
    
    public void EquipItem()
    {
        Console.WriteLine("\n[아이템 목록]\n");

        for (int i = 0; i < Items.Count; i++)
        {
            string equippedMarker = Items[i].IsEquipped ? "[E]" : " ";
            Console.WriteLine($"{i + 1}. {equippedMarker} {Items[i].Name}");
        }

        Console.Write("\n장착하거나 해제할 아이템의 번호를 입력해 주세요. ");
        string input = Console.ReadLine();
        int choice;

        if (!int.TryParse(input, out choice) || choice < 0 || choice > Items.Count)
        {
            Console.WriteLine("잘못된 입력입니다.");
            return;
        }

        if (choice == 0) return; // 취소

        // 선택한 아이템 장착/해제
        Item selectedItem = Items[choice - 1];
        selectedItem.IsEquipped = !selectedItem.IsEquipped;

        string status = selectedItem.IsEquipped ? "장착" : "해제";
        Console.WriteLine($"{selectedItem.Name}을(를) {status}했습니다.");
    }
}

class Item
{
    public string Name { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public bool IsEquipped { get; set; }  // 장착 여부

    public Item(string name, int attack, int defense, int price, string description)
    {
        Name = name;
        Attack = attack;
        Defense = defense;
        Price = price;
        Description = description;
        IsEquipped = false;  // 기본값: 장착 안 함
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
        
        Character player = new Character("르탄", "전사"); // 캐릭터 객체 생성
        Inventory inventory = new Inventory();        // 인벤토리 객체 생성
        
        // 기본 아이템 추가 (나중에 상점에서 구매하면 추가할 수도 있음)
        inventory.AddItem(new Item("무쇠갑옷", 0, 5, 300, "무쇠로 만들어져 튼튼한 갑옷입니다."));
        inventory.AddItem(new Item("스파르타의 창", 7, 0, 500, "스파르타 전사들이 사용한 전설의 창입니다."));
        inventory.AddItem(new Item("낡은 검", 2, 0, 100, "쉽게 볼 수 있는 낡은 검입니다."));
        
        while (true)
        {
            Console.WriteLine("\n1. 상태 보기 \n2. 인벤토리 \n3. 상점\n");
            Console.Write("원하시는 행동을 입력해 주세요. ");
            
            string input1 = Console.ReadLine();
            int num1;

            if (!int.TryParse(input1, out num1))
            {
                Console.WriteLine("잘못된 입력입니다. 숫자를 입력해 주세요.");
                continue;
            }

            switch (num1)
            {
                case 1:
                    while (true)
                    {
                        Console.WriteLine("\n1. 상태보기를 선택하셨습니다.\n");
                        player.ShowStatus();
                        Console.WriteLine("\n0. 나가기 \n");
                        Console.Write("원하시는 행동을 입력해주세요. ");
                    
                        string input2 = Console.ReadLine();
                        int num2;
                        
                        if (!int.TryParse(input2, out num2) || num2 != 0)
                        {
                            Console.WriteLine("잘못된 입력입니다. 다시 입력해 주세요.");
                            continue;
                        }
                        break;
                    }
                    break;
                case 2:
                    while (true)
                    {
                        inventory.ShowInventory();

                        Console.WriteLine("\n2. 인벤토리를 선택하셨습니다.");
                        Console.WriteLine("\n[아이템 목록]\n\n1. 장착 관리\n0. 나가기\n");
                        Console.Write("원하시는 행동을 입력해 주세요. ");
                        
                        string input3 = Console.ReadLine();
                        int action;

                        if (!int.TryParse(input3, out action))
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            continue;
                        }

                        if (action == 1)
                        {
                            inventory.EquipItem();  // 장착 기능 추가
                        }
                        else if (action == 2)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }
                    break;
                case 3:
                    Console.WriteLine("\n3. 상점을 선택하셨습니다.\n상점 기능은 아직 구현되지 않았습니다.");
                    break;
                default:
                    Console.WriteLine("\n잘못된 입력입니다. 다시 입력해 주세요.");
                    continue;
            }
        }
    }
}