import json
import sqlite3

json_file_path = r'C:\localrepos\DrawingNumberGenerator\Client\wwwroot\Data\pdf_drawings.json'
db_file_path = r'C:\localrepos\DrawingNumberGenerator\DNG.Library\Data\SB_Plastic_Belt_CAD_Automation.db'

with open(json_file_path, 'r') as file:
    data = json.load(file)

conn = sqlite3.connect(db_file_path)
cursor = conn.cursor()

def extract_files(directory, category, cursor):
    if isinstance(directory, list):
        for file_data in directory:
            cursor.execute('''
                INSERT INTO DWGs (filename, relative_path, size_mb, created_date, category)
                VALUES (?, ?, ?, ?, ?)
            ''', (file_data['filename'], file_data['relative_path'], file_data['size_mb'], file_data['created_date'], category))
    elif isinstance(directory, dict):
        for key, value in directory.items():
            if key == 'files':
                extract_files(value, category, cursor)
            else:
                extract_files(value, category + '\\' + key if category else key, cursor)

extract_files(data['directory_structure'], '', cursor)

conn.commit()
conn.close()
