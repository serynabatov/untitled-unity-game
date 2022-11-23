import json

import download_files

if __name__ == "__main__":
    with open("folder_names.json") as file:
        data = json.load(file)

        for folder_name in data:
            download_files.download_folder(folder_name)