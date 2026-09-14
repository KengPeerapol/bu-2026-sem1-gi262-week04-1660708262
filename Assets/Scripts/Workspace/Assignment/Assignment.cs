using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assignment
{
    public class Assignment : MonoBehaviour
    {
        public void Start()
        {
            // AS01_CountWords();
            // AS02_CountNumber();
            // AS03_CheckValidBrackets();
            // AS04_PrintReverseLinkedList();
            // AS05_FindMiddleElement();
            // AS06_MergeDictionaries();
            // AS07_RemoveDuplicatesFromLinkedList();
            // AS08_TopFrequentNumber();
            // AS09_PlayerInventory();
            // AS10_GameEventQueue();
            // AS11_PlayerStatsTracker();
        }

        #region Assignment

        [Header("AS01 - Count Words")]
        [SerializeField] private string[] as01Words;

        public void AS01_CountWords()
        {
            string[] words = as01Words;
            if (words == null || words.Length == 0)
            {
                Debug.Log("No words to count");
                return;
            }

            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            foreach (string word in words)
            {
                string cleanWord = word.Trim();

                if (cleanWord.Length == 0)
                    continue;

                if (wordCount.ContainsKey(cleanWord))
                    wordCount[cleanWord]++;
                else
                    wordCount[cleanWord] = 1;
            }

            // format output ให้ตรง test case
            foreach (KeyValuePair<string, int> entry in wordCount)
            {
                Debug.Log($"word: '{entry.Key}' count: {entry.Value}");
            }
        }

        [Header("AS02 - Count Number")]
        [SerializeField] private int[] as02Numbers;

        public void AS02_CountNumber()
        {
            int[] numbers = as02Numbers;
            if (numbers == null || numbers.Length == 0)
            {
                Debug.Log("No numbers to count");
                return;
            }

            Dictionary<int, int> numCount = new Dictionary<int, int>();

            foreach (int num in numbers)
            {
                if (numCount.ContainsKey(num))
                    numCount[num]++;
                else
                    numCount[num] = 1;
            }

            foreach (KeyValuePair<int, int> entry in numCount)
            {
                Debug.Log($"number: {entry.Key} count: {entry.Value}");
            }
        }

        [Header("AS03 - Check Valid Brackets")]
        [SerializeField] private string as03Input;

        public void AS03_CheckValidBrackets()
        {
            string input = as03Input;
            if (input == null)
            {
                Debug.Log("Invalid");
                return;
            }

            Dictionary<char, char> bracketPairs = new Dictionary<char, char>()
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' }
            };

            LinkedList<char> stack = new LinkedList<char>();

            foreach (char c in input)
            {
                // เจอวงเล็บเปิด → push
                if (bracketPairs.ContainsKey(c))
                {
                    stack.AddLast(c);
                }
                // เจอวงเล็บปิด → check
                else if (bracketPairs.ContainsValue(c))
                {
                    if (stack.Count == 0)
                    {
                        Debug.Log("Invalid");
                        return;
                    }

                    char lastOpen = stack.Last.Value;
                    if (bracketPairs[lastOpen] == c)
                    {
                        stack.RemoveLast(); // คู่ตรง → pop
                    }
                    else
                    {
                        Debug.Log("Invalid");
                        return;
                    }
                }
            }

            // ถ้า stack ยังเหลือ แสดงว่ามีวงเล็บเปิดที่ไม่ปิด
            if (stack.Count == 0)
                Debug.Log("Valid");
            else
                Debug.Log("Invalid");
        }

        [Header("AS04 - Print Reverse Linked List")]
        [SerializeField] private IntLinkedListInput as04List = new IntLinkedListInput();

        public void AS04_PrintReverseLinkedList()
        {
            LinkedList<int> list = as04List.GetLinkedList();
            if (list == null || list.Count == 0)
            {
                Debug.Log("List is empty");
                return;
            }

            LinkedListNode<int> current = list.Last;

            while (current != null)
            {
                Debug.Log(current.Value);
                current = current.Previous;
            }
        }

        [Header("AS05 - Find Middle Element")]
        [SerializeField] private StringLinkedListInput as05List = new StringLinkedListInput();

        public void AS05_FindMiddleElement()
        {
            LinkedList<string> list = as05List.GetLinkedList();
            if (list == null || list.Count == 0)
            {
                Debug.Log("List is empty");
                return;
            }

            LinkedListNode<string> slow = list.First;
            LinkedListNode<string> fast = list.First;

            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;       // เดิน 1 ก้าว
                fast = fast.Next.Next; // เดิน 2 ก้าว
            }

            Debug.Log(slow.Value);
        }

        [Header("AS06 - Merge Dictionaries")]
        [SerializeField] private StringIntDictionaryInput as06FirstDictionary = new StringIntDictionaryInput();
        [SerializeField] private StringIntDictionaryInput as06SecondDictionary = new StringIntDictionaryInput();

        public void AS06_MergeDictionaries()
        {
            Dictionary<string, int> dict1 = as06FirstDictionary.GetDictionary();
            Dictionary<string, int> dict2 = as06SecondDictionary.GetDictionary();

            if (dict1 == null || dict2 == null)
            {
                Debug.Log("One or both dictionaries are null");
                return;
            }

            Dictionary<string, int> merged = new Dictionary<string, int>(dict1);

            foreach (var kvp in dict2)
            {
                if (merged.ContainsKey(kvp.Key))
                {
                    merged[kvp.Key] += kvp.Value;
                }
                else
                {
                    merged[kvp.Key] = kvp.Value;
                }
            }

            foreach (var kvp in merged)
            {
                Debug.Log($"key: {kvp.Key}, value: {kvp.Value}");
            }
        }

        [Header("AS07 - Remove Duplicates From Linked List")]
        [SerializeField] private IntLinkedListInput as07List = new IntLinkedListInput();

        public void AS07_RemoveDuplicatesFromLinkedList()
        {
            LinkedList<int> list = as07List.GetLinkedList();
            if (list == null || list.Count == 0)
            {
                return; // เทสไม่ได้เช็ค empty case
            }

            HashSet<int> seen = new HashSet<int>();
            var current = list.First;

            while (current != null)
            {
                var nextNode = current.Next;

                if (seen.Contains(current.Value))
                {
                    list.Remove(current); // ลบค่าที่ซ้ำ
                }
                else
                {
                    seen.Add(current.Value);
                }

                current = nextNode;
            }

            // ✅ แก้ format การแสดงผล
            string result = "";
            foreach (var item in list)
            {
                result += item + "\n";
            }

            result = result.TrimEnd('\n'); // เอา \n ท้ายออก
            Debug.Log(result);
        }

        [Header("AS08 - Top Frequent Number")]
        [SerializeField] private int[] as08Numbers;

        public void AS08_TopFrequentNumber()
        {
            int[] numbers = as08Numbers;
            if (numbers == null || numbers.Length == 0)
            {
                Debug.Log("Input array is empty");
                return;
            }

            // นับความถี่ของแต่ละตัว
            Dictionary<int, int> freq = new Dictionary<int, int>();
            foreach (int num in numbers)
            {
                if (freq.ContainsKey(num))
                    freq[num]++;
                else
                    freq[num] = 1;
            }

            // หา max frequency
            int maxCount = freq.Values.Max();

            // เลือกตัวที่ความถี่มากที่สุด (ถ้า tie เอาตัวแรกที่เจอใน input)
            int topNumber = numbers.First(n => freq[n] == maxCount);

            // ✅ ตรง format ที่เทสต้องการ
            Debug.Log($"{topNumber} count: {maxCount}");
        }

        [Header("AS09 - Player Inventory")]
        [SerializeField] private StringIntDictionaryInput as09Inventory = new StringIntDictionaryInput();
        [SerializeField] private string as09ItemName;
        [SerializeField] private int as09Quantity;

        public void AS09_PlayerInventory()
        {
            Dictionary<string, int> inventory = as09Inventory.GetDictionary();
            string itemName = as09ItemName;
            int quantity = as09Quantity;

            if (inventory == null)
            {
                Debug.Log("Inventory is null");
                return;
            }

            // ถ้ามี item อยู่แล้ว บวกเพิ่ม
            if (inventory.ContainsKey(itemName))
            {
                inventory[itemName] += quantity;
            }
            else
            {
                // ถ้าไม่มี item ให้เพิ่มใหม่
                inventory[itemName] = quantity;
            }

            // ✅ format: แสดงทุก item ตาม dictionary
            foreach (var kvp in inventory)
            {
                Debug.Log($"{kvp.Key}: {kvp.Value}");
            }
        }

        [Header("AS10 - Game Event Queue")]
        [SerializeField] private GameEventLinkedListInput as10EventQueue = new GameEventLinkedListInput();

        public void AS10_GameEventQueue()
        {
            LinkedList<GameEvent> eventQueue = as10EventQueue.GetLinkedList();

            if (eventQueue == null || eventQueue.Count == 0)
            {
                Debug.Log("Event queue is empty");
                return;
            }

            while (eventQueue.Count > 0)
            {
                GameEvent nextEvent = eventQueue.First.Value;
                eventQueue.RemoveFirst();

                Debug.Log($"Processing event: {nextEvent.Name}");
                Debug.Log($"Remaining events in queue: {eventQueue.Count}");

                switch (nextEvent.EventType.ToLower())
                {
                    case "enemy":
                        Debug.Log($"Enemy event processed - {nextEvent.Name}");
                        break;
                    case "powerup":
                        Debug.Log($"Power-up event processed - {nextEvent.Name}");
                        break;
                    case "level":
                        Debug.Log($"Level event processed - {nextEvent.Name}");
                        break;
                    case "achievement":
                        Debug.Log($"Achievement unlocked - {nextEvent.Name}");
                        break;
                    case "unknown":
                        Debug.Log($"Generic event processed - {nextEvent.Name}");
                        break;
                    default:
                        Debug.Log($"Unhandled event type: {nextEvent.EventType} - {nextEvent.Name}");
                        break;
                }
            }
        }

        [Header("AS11 - Player Stats Tracker")]
        [SerializeField] private StringIntDictionaryInput as11PlayerStats = new StringIntDictionaryInput();
        [SerializeField] private string as11StatName;
        [SerializeField] private int as11Value;

        public void AS11_PlayerStatsTracker()
        {
            Dictionary<string, int> playerStats = as11PlayerStats.GetDictionary();
            string statName = as11StatName;
            int value = as11Value;

            if (playerStats == null)
            {
                Debug.Log("Player stats is null");
                return;
            }

            // อัปเดตค่า stat
            if (playerStats.ContainsKey(statName))
            {
                playerStats[statName] += value;
            }
            else
            {
                playerStats[statName] = value;
            }

            int newValue = playerStats[statName];
            Debug.Log($"Updated {statName}: {newValue}");

            // แสดงผลสถิติทั้งหมด
            Debug.Log("Current player statistics:");
            foreach (var stat in playerStats)
            {
                Debug.Log($"{stat.Key}: {stat.Value}");
            }
        }

        #endregion
    }
}
