#include<iostream>
#include<fstream>
#include<string>
#include<queue>
#include<list>
using namespace std;

int M,N,T;
string *states,*rules,*test;		//pointer for states,rules and test_cases
int **TM;							//double pointer to store transition matrix

class Node							//initialize class Node
{
public:
	int state;						//state of current node
	queue<int> ls;					//to store squence of all the action from initial state to desire state	
};
void readFromfile();                                                //read data from file
void Output(int *,int);                                             //print result on console
bool Contains(int st,list<Node>);									//check the state in history queue
Node addChildrenToQueue(Node &n,queue<Node> &,list<Node> &,int);		//add child to queue
bool isGoal(Node &n,int goalState);									//compare current state to goal state and retun bool
Node findGoal(Node &n,int goalState,queue<Node> &,list<Node> &);    //return the final node with squence of action

int main()
{
	readFromfile();
	for(int i=0;i<T*2;i=i+2)
	{
		
		queue<Node> Q;
		list<Node> H;
		int initial_state;
		int final_state;
		for(int j=0;j<M;j++)
		{
			if(states[j]==test[i])
			{
				initial_state=j;
			}	
		}
		for(int j=0;j<M;j++)
		{
			if(states[j]==test[i+1])
			{
				final_state=j;
			}
		}
		if(initial_state==final_state)
		{
			cout<<"Agent is already on desire state"<<endl;
		}
		else
		{
		Node n1;
		n1.state=initial_state;
		Node nd=findGoal(n1,final_state,Q,H);
		if(nd.state==-1)
		{
			cout<<"Goal is unreachable "<<endl;
			
		}
		else
		{
			int a=nd.ls.size();
			int *result=new int[a];
			for(int k=0;k<a;k++)
			{
				result[k]=nd.ls.front();
				nd.ls.pop();
			}
			Output(result,a);
			delete[] result;
			result=0;
			
		}
		}
	}
	
	delete[] states;                                          //
	states=NULL;                                              //
	delete[] rules;                                           //
	rules=NULL;                                               //
	delete[] test;                                            //
	test=NULL;                                                //deallocate memory and remove dangling pointers
	for(int i=0;i<M;i++)                                      //
	{                                                         //
		delete[] TM[i];                                       //
	}                                                         //
	delete[] TM;                                              //
	TM=NULL;                                                  //
	system("pause");
	return 0;
}

void Output(int *arr,int a)
{
	for(int i=0;i<a;i++)
	{
		for(int j=0;j<N;j++)
		{
			if(arr[i]==j)
			{
				cout<<rules[j];
			}
		}
		if(i<a-1)
			cout<<"->";
	}
	cout<<endl;
}
Node findGoal(Node &n,int goalState,queue<Node> &Q,list<Node> &H)
{
	Node b;
	b.state=-1;
	Q.push(n);
	while(true)
	{
		if(Q.empty())
		{
			return b;
		}
		n=Q.front();
		H.push_front(n);
		Q.pop();
		Node bl=addChildrenToQueue(n,Q,H,goalState);
		if(bl.state==-2)
		{
			continue;
		}
		else
			return bl;
	}
}
bool isGoal(Node &n,int goalState)
{
	return n.state==goalState;
}
Node addChildrenToQueue(Node &n,queue<Node> &Q,list<Node> &H,int goalState)
{
	for(int i=0;i<N;i++)
	{
		if(!Contains(TM[n.state][i],H))
		{

			Node t;
			t.state=TM[n.state][i];
			t.ls=n.ls;
			t.ls.push(i);
			if(isGoal(t,goalState))
				return t;
			Q.push(t);
		}
	}
	Node f;
	f.state=-2;
	return f;
}
bool Contains(int st,list<Node> H)
{
	int a=H.size();
	Node n;
	for(int i=0;i<a;i++)
	{
		n=H.front();
		H.pop_front();
		if(st==n.state)
			return true;
	}
	return false;
}

void readFromfile()       //reading data from file
{
	ifstream fin;
	fin.open("data.txt");
	
	fin>>M>>N>>T;
	states=new string[M];	//allocating string array to store states
	rules=new string[N];	//allocating string array to store rules
	test=new string[T*2];	//allocating string array to store test cases 
	TM=new int*[M];         //allocating a 2D array to store transition matrix
	for(int i=0;i<M;i++)
	{
		TM[i]=new int[N];
	}
	fin.ignore();

	for(int i=0;i<M;i++)  //read states
	{
		fin>>states[i];
	}
	fin.ignore();

	for(int i=0;i<N;i++)	//read rules
	{
		fin>>rules[i];
	}
	fin.ignore();

	for(int i=0;i<M;i++)	//read transition matrix
	{
		for(int j=0;j<N;j++)
		{
			fin>>TM[i][j];
		}
	}
	fin.ignore();
	for(int i=0;i<T*2;i++)	//read test cases
	{
		fin>>test[i];
	}
	fin.close();

}


