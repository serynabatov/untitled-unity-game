import google.oauth2.credentials
import google_auth_oauthlib.flow

def authorize():
    # Use the client_secret.json file to identify the application requesting
    # # authorization. The client ID (from that file) and access scopes are required.
    flow = google_auth_oauthlib.flow.Flow.from_client_secrets_file(
        'client_secret.json',
        scopes=['https://www.googleapis.com/auth/drive.metadata.readonly'])

    # Indicate where the API server will redirect the user after the user completes
    # # the authorization flow. The redirect URI is required. The value must exactly
    # # match one of the authorized redirect URIs for the OAuth 2.0 client, which you
    # # configured in the API Console. If this value doesn't match an authorized URI,
    # # you will get a 'redirect_uri_mismatch' error.
