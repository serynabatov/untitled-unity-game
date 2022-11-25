from __future__ import print_function

import io
from googleapiclient.discovery import build
from googleapiclient.errors import HttpError
from googleapiclient.http import MediaIoBaseDownload

import login


def download_folder(folder_name):
    """Downloads a file
    Args:
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
            results = service.files().list(
                q="mimeType='application/vnd.google-apps.folder' and name = '{}'".format(folder_name),
                spaces="drive",
            ).execute()
            print(folder_name)
            for file in results.get('files', []):
                file_list = service.files().list(
                    q="'{}' in parents".format(file.get("id"))
                ).execute()
                print(file_list)
                # for f in file_list.get("files"):
                #     # pylint: disable=maybe-no-member
                #     request = service.files().get_media(fileId=f.get("id"))
                #     real_file = io.BytesIO()
                #     downloader = MediaIoBaseDownload(real_file, request)
                #     done = False
                #     while done is False:
                #         status, done = downloader.next_chunk()
                #         print(F'Download {int(status.progress() * 100)}.')
                #         save_file(f.get("name"), real_file.getvalue())

            page_token = results.get('nextPageToken', None)
            if page_token is None:
                break

    except HttpError as error:
        # TODO(developer) - Handle errors from drive API
        print(f'An error occurred: {error}')


# TODO discuss how the data should be stored here
def save_file(file_name, file_content):
    with open(file_name, "w") as file:
        file.writelines(str(file_content))


if __name__ == '__main__':
    download_folder("example")
