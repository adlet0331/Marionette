# Need to install openpyxl
# pip install openpyxl
# python .\Assets\Scripts\excel_to_json.py path to file
# Example: python .\Assets\Scripts\excel_to_json.py Assets data UnitData EnemyData

from openpyxl import load_workbook
from openpyxl.utils import get_column_letter
import json
import os

file_path = os.getcwd()
file_path = os.path.join(file_path, 'DataBase')

excel_file_path =  file_path + '.xlsx'
json_file_path = file_path + '.json'

wb = load_workbook(excel_file_path)
itemWS = wb.worksheets[0]
scriptWS = wb.worksheets[1]
lockObjectWS = wb.worksheets[2]
talkWS = wb.worksheets[3]

len_list = []
len_list.append(int(itemWS['B1'].value))
len_list.append(int(scriptWS['B1'].value  ))
len_list.append(int(lockObjectWS['B1'].value  ))
len_list.append(int(talkWS['B1'].value))

'''
for row in range(1, last_row + 1): 
    my_dict = {}
    for column in range(1, last_column + 1):
        column_letter = get_column_letter(column)
        if row > 1:
            my_dict[excelWS[column_letter + str(1)].value] = excelWS[column_letter + str(row)].value
    my_list.append(my_dict)
my_list.remove({})

data = json.dumps(my_list, sort_keys=True, indent=4)
with open(json_file_path, 'w', encoding='utf-8') as f:
    f.write(data)
'''