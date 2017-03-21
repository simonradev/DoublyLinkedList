# DoublyLinkedList

This is my first implementation of the doubly linked list. It is a dynamic list where you know only the first and
the last element. A single element holds it's value and a reference to the previous and the next element. If you want 
to get for ex. the middle element you start to go from reference to reference untill you find the one that you want. It is
implemented with a generic class which means that you can work with whatever data type you want. Everything is encapsulated 
in the class so you dont need to worry about all that things. Everything you need to do is just acces a method or a 
property with the "." operator read what it does in the description check if it is the one you want and if it is just
give it the arguments it needs.

These are the implemented methods:
- AddAtIndex();
- AddInTheBeginning();
- AddInTheEnd();
- AddRange();
- Contains();
- GetElementByIndex();
- GetIndex();
- RemoveAll();
- RemoveAt();
- RemoveFirst();
- RemoveLast();

This is the only overriden method:
- ToString();

These are the implemented properties:
- Count;
- [int index] - With this property you can get and set elements like in a normal array.
		You can also add an element by index but only in the end.
		That means you can add only at the index which is equal to the current
		count of the list. Everything else will cause an exception.