import {execFileSync} from 'node:child_process';
import {rmSync} from 'node:fs';
import {fileURLToPath} from 'node:url';

const site = fileURLToPath(new URL('../', import.meta.url));
rmSync(new URL('../api', import.meta.url), {recursive: true, force: true});
execFileSync('dotnet', ['tool', 'run', 'docfx', 'metadata', 'docfx/docfx.json'], {cwd: site, stdio: 'inherit'});
execFileSync(process.execPath, ['scripts/docfx-postprocess.mjs'], {cwd: site, stdio: 'inherit'});
