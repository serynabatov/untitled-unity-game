from __future__ import print_function

import io
from googleapiclient.discovery import build
from googleapiclient.errors import HttpError
from googleapiclient.http import MediaIoBaseDownload
from pathlib import Path
import login


def download_folder(folder_name, whole_path=None):
    """Downloads a file
    Args:
        whole_path:
        folder_name: name of the folder
    Returns :

    Load pre-authorized user credentials from the environment.
    """
    creds = login.login()

    try:
        service = build('drive', 'v3', credentials=creds)
        page_token = None
        while True:
            # Call the Drive v3 API
            if whole_path == "../Assets/Sprites/":
                results = service.files().list(
                    q="mimeType='application/vnd.google-apps.folder' and name = '{}'".format(
                        "Sprites"),
                    spaces="drive",
                ).execute()
            else:
                parent_folder = service.files().list(
                    q="mimeType='application/vnd.google-apps.folder' and name = '{}'".format(
                        whole_path.split("/")[-2]),
                    spaces="drive",
                ).execute()
                results = service.files().list(
                    q="mimeType='application/vnd.google-apps.folder' and name = '{}' and '{}' in parents".format(
                        folder_name, parent_folder.get('files', [])[0].get('id')),
                    spaces="drive",
                ).execute()

            Path("./{}".format(whole_path)).mkdir(parents=True, exist_ok=True)
            for file in results.get('files', []):
                file_list = service.files().list(
                    q="'{}' in parents".format(file.get("id"))
                ).execute()

                for f in file_list.get("files"):
                    # pylint: disable=maybe-no-member
                    if f.get("mimeType") != "application/vnd.google-apps.folder":
                        file_name = f.get("name")
                        if not Path(f"{whole_path}/{file_name}").is_file():
                            print(f"{whole_path}/{file_name}")
                            request = service.files().get_media(fileId=f.get("id"))
                            real_file = io.BytesIO()
                            downloader = MediaIoBaseDownload(real_file, request)
                            done = False
                            while done is False:
                                status, done = downloader.next_chunk()
                                print(F'Download {int(status.progress() * 100)}.')
                                save_file(f.get("name"), whole_path,
                                          real_file.getvalue())
                        print(f"File {file_name} exists")

            page_token = results.get('nextPageToken', None)
            if page_token is None:
                break

    except HttpError as error:
        # TODO(developer) - Handle errors from drive API
        print(f'An error occurred: {error}')


# TODO discuss how the data should be stored here
def save_file(file_name, folder_name, file_content):
    with open(f"./{folder_name}/{file_name}", "wb") as file:
        file.write(file_content)


if __name__ == '__main__':
    download_folder("example")
