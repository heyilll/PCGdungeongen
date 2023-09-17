import subprocess
import random
import plotly.express as px
import pandas as pd
import time

separatingAdj = " adj"
separatingRooms = " sprite" 
lnvalues = list()
malvalues = list()

class TreeNode:
    children = []
    id: int
    keys = []
	
    def __init__(self, node_id):
        self.children = []
        self.id = node_id
        self.keys = []

min_nodes = 7
# nodes = []
max_keys_per_node = 2

def generatet(max) -> list[TreeNode]:	
    nodes = []
    
    for i in range(random.randint(min_nodes, max)):
        new_node = TreeNode(i)
		
        if i > 0:
            parent = nodes[random.randint(0, i - 1)]
            key_location = select_key_location(nodes,i)
			
            parent.children.append(i)
            key_location.keys.append(i)
			
        nodes.append(new_node)
    return nodes    

def select_key_location(nodes: list, i: int) -> TreeNode:
	available_nodes = nodes[0:i - 1:1]
	random.shuffle(available_nodes)
	
	for j in range(0, i - 1):
		candidate = available_nodes[j]
		if len(candidate.keys) < max_keys_per_node:
			return candidate
	
	return nodes[random.randint(0, i - 1)]

def gen():
    rand = random.randrange(0,32767)
    command = "clingo -n 1 --rand-freq=1 --seed=" + str(rand) + " dungeon-core.lp dungeon-style.lp"
    result = subprocess.run(command, capture_output=True, text=True).stdout
    return result

def parse(dungeon):
    global lnvalues
    global malvalues
    dungeon = str(dungeon)
    firstDIndex = dungeon.find("adj")
    firstSpIndex = dungeon.find("sp")
    
    abr = dungeon[firstSpIndex: firstDIndex]
    spr = abr.split(separatingRooms)
    rooms = spr
    
    ln = lenien(rooms)
    lnvalues.append(ln)  
    mal = mapl(rooms)
    malvalues.append(mal)
    return 
    
def lenien(dunarr):
    allr = len(dunarr)
    path = 0
    safe = 0
    ans = 0
             
    for x in dunarr:
        if 'path' in x:
            path += 1
        elif 'treasure' in x:
            safe += 1  
    
    ans = (safe)/(allr - path)        
    return ans

def mapl(dunarr):
    allr = len(dunarr)
    path = 0
    
    for x in dunarr:
        if 'path' in x:
            path += 1
            
    nodes = generatet(allr-path)
    onex =0
    twox=0
    threex=0
    
    alln  = len(nodes)
    
    for x in nodes:
        p = len(x.children)
        if p == 1:
            onex +=1 
        elif p == 2:
            twox += 1    
        else:
            threex += 1    
            
    ans = ((1*onex)+(0.5*twox)+(0*threex))/alln   
    return ans
 
def timed():
    # current timestamp
    x = time.time()
    print("Timestamp:", x)
          
for x in range(1000):
    dung = gen()
    parse(dung)
    
zipped = list(zip(malvalues, lnvalues))
df = pd.DataFrame(zipped, columns=['Map Linearity','Leniency'])

fig = px.density_heatmap(df, x="Leniency", y="Map Linearity", nbinsx=80, nbinsy=40)
fig.update_layout(
    autosize=False,
    width=800,
    height=800,
    font_size = 17)
fig.update_yaxes(title_font=dict(size=20),tickfont = dict(size=17))
fig.update_xaxes(title_font=dict(size=20),tickfont = dict(size=17))
fig.write_html("result.html")
fig.show()